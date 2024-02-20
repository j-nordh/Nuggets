import paho.mqtt.client as mqtt
from decode import decode
import  json
import atexit

def on_message(client, userdata, message):
    # print("message received " ,str(message.payload.decode("utf-8")))
    print("message topic=",message.topic)
    print("message qos=",message.qos)
    print("message retain flag=",message.retain)

    #print(message.payload)
    #Decide and publish
    str_msg = str(message.payload.decode("utf-8"))
    d_v = decode(str_msg)
    d_v_str = json.dumps(d_v)
    client.publish(topic="outTopic", payload=d_v_str)

def exit_handler():
    client.loop_stop()
    print('ending')

if __name__ == "__main__":

    client_name = "HAN_decoder"
    broker_address = "localhost"


    client = mqtt.Client(client_name)
    client.on_message=on_message

    atexit.register(exit_handler)

    print("connecting to broker")
    client.connect(broker_address, 1883) #connect to broker
    client.subscribe("my/inTopic")
    print("Subbed to inTopic...awaiting messages")

    client.loop_start()

    while True:
        pass

    # filename = r"/home/pi/scripts/telegram.txt"

    # with open(filename, "r") as f:
    #     data = f.read()

    # d_v = decode(data)

    # d_v_str = json.dumps(d_v)

    # client.publish(topic="outTopic", payload=d_v_str)