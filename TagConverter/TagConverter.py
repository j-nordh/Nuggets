import json
import re
import datetime

def getLines(path):
    f = open(path, "rb")
    text = f.read()
    return text.decode('utf-16').splitlines()

def populateOldNumbers(oldNumbers):
    print('test')

def isTag(statement):
    if re.match("^INSERT\s*\[dbo\].\[tblTags\]", statement, re.IGNORECASE):
        return True
    return False

def isTagValue(statement):
    if re.match("^INSERT\s*\[dbo\].\[tblTagValues\]", statement, re.IGNORECASE):
        return True
    return False

def getTagId(statement):
    value = re.match('.*VALUES\s*\(([0-9]+)', statement, re.IGNORECASE)
    return value.group(1)

def getTagNumber(statement):
    value = re.match(".*VALUES\s*\([0-9]+\s*,\s*[0-9]+\s*,\s*N*'(.*?)'", statement, re.IGNORECASE)
    return value.group(1)

def replaceTagId(statement, replacement):
    value = re.sub("(.*VALUES\s*\()([0-9]+)(.*)", f"\g<1>{replacement}\g<3>", statement, re.IGNORECASE)
    return value

def replaceTagNumber(statement, replacement):
    value = re.sub("(.*VALUES\s*\([0-9]+\s*,\s*[0-9]+\s*,\s*N*')(.*?)('.*)", f"\g<1>{replacement}\g<3>", statement, re.IGNORECASE)
    return value

def getTagLines(lines):
    return list(filter(isTag, lines))

def getTagValueLines(lines):
    return list(filter(isTagValue, lines))

def getNewNumber(oldNumber, oldNumbers, newNumbers):
    name = oldNumbers.get(oldNumber)
    return newNumbers.get(name)

def addNumbersToTag(dictionary: dict, tagLines):
    for line in tagLines:
        dictionary[getTagId(line)] = getTagNumber(line)

def addTagsToNumbers(dictionary: dict, tagLines):
    for line in tagLines:
        dictionary[getTagNumber(line)] = getTagId(line)

def getTimestamp(statement):
    value = re.match("^INSERT.*CAST\s*\(s*N'\s*(.*?)'\s*AS\s*DateTime", statement, re.IGNORECASE)
    return value.group(1)

def getTagValue(statement):
    value = re.match("^INSERT.*N\s*'(.*?)'\s*?\)$", statement, re.IGNORECASE)
    #^INSERT.*N\s*'(-*?[0-9]+\.*?[0-9]*)'\)$
    return value.group(1)

def getWeatherValues(tagValues, oldNumbers, weatherTags):
    weatherDict = {}
    for tag in tagValues:
        #print(tag)
        #print('Kom hit')
        tagId = getTagId(tag)
        tagNumber = oldNumbers.get(tagId)
        #print(tagNumber)
        if tagNumber in weatherTags:
            timestamp = getTimestamp(tag)
            weatherData = weatherDict.get(timestamp)
            if weatherData == None:
                weatherData = {}
                weatherDict[timestamp] = weatherData
            
            #print(tag)
            weatherData[tagNumber] = getTagValue(tag)
    return weatherDict

def printWeatherValue(weatherItem: tuple):
    timestamp = weatherItem[0]
    weatherData = weatherItem[1]

    line1 = f"INSERT [dbo].[tblWeatherForecasts] (ComplexId, Timestamp) VALUES (1, CAST(N'{timestamp}' AS DateTime))"
    line2 = "set @forecastId = SCOPE_IDENTITY()"
    line3 = f"INSERT [dbo].[tblWeatherForecastValues](WeatherForecastId, [Timestamp], [Temperature], [WindSpeed], [MeanPrecipitation], [CloudCoverLow], [CloudCoverMedium], [CloudCoverHigh], [CloudCoverTotal], [Symbol]) VALUES (@forecastId, CAST(N'{timestamp}' AS DateTime), N'{weatherData['t']}', N'{weatherData['ws']}', N'{weatherData['pmean']}', N'{weatherData['lcc_mean']}', N'{weatherData['mcc_mean']}', N'{weatherData['hcc_mean']}', N'{weatherData['tcc_mean']}', N'{weatherData['Wsymb2']}')"
    
    # VALUES (@forecastId, @Timestamp, @temp, @WindSpeed, @MeanPrecipitation, @CCLow, @CCMed, @CCHigh, @CCTotal,@Symbol)
    
    lst = [line1, line2, line3]
    return lst

def testTuple():
    return (1,2)

def isCompleteWeather(weatherData: dict, weatherTags):
    for tag in weatherTags:
        if tag not in weatherData.keys():
            return False
    return True


def writeNewTagValues(tagValues, oldNumbers, newNumbers):
    newValues = []

    for value in tagValues:
        tagId = getTagId(value)
        newId = getNewNumber(tagId, oldNumbers, newNumbers)
        if(newId != None):
            newStatement = replaceTagId(value, newId)
            newValues.append(newStatement)

    f = open("newValues.sql", "w")
    for line in newValues:
        f.write(line+'\n')
    f.close()

def writeWeatherValues(tagValues, oldNumbers, weatherTags):
    weather = getWeatherValues(tagValues, oldNumbers, weatherTags)

    f = open("newWeatherValues.sql", "w")
    f.write("declare @forecastId bigint"+'\n'+'\n')
    for item in weather.items():
        lines = printWeatherValue(item)
        for line in lines:
            f.write(line+'\n')
        f.write('\n')
    f.close()

def main():

    startTime = datetime.datetime.now()

    with open('config.json') as txt:
        data = json.load(txt)
    

    oldTagsPath = data['oldTagsPath']
    oldTagValuesPath = data['oldTagValuesPath']
    newTagsPath = data['newTagsPath']
    weatherTags = data['WeatherTags']

    oldNumbers = {}
    newNumbers = {}

    #Read lines from each file
    oldTagLines = getLines(oldTagsPath)
    oldValLines = getLines(oldTagValuesPath)
    newTagLines = getLines(newTagsPath)

    #Parse appropriate lines from each file
    oldTags = getTagLines(oldTagLines)
    tagValues = getTagValueLines(oldValLines)
    newTags = getTagLines(newTagLines)

    #Populate dictionaries with new and old tag numbers and ids
    addNumbersToTag(oldNumbers, oldTags)
    addTagsToNumbers(newNumbers, newTags)

    weather = getWeatherValues(tagValues, oldNumbers, weatherTags)

    # test = True
    # for w in weather.values():
    #     test = test and isCompleteWeather(w, weatherTags)
    # print(test)

    endtime = datetime.datetime.now()

    diff =  endtime - startTime

    writeNewTagValues(tagValues, oldNumbers, newNumbers)

    writeWeatherValues(tagValues, oldNumbers, data['WeatherTags'])

    #print(f"Well done, time for completion: {diff} :)")

main()