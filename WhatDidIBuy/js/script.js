function searchTable() {
  // Declare variables
  var input, filter, table, tr, item, i, itemValue, arr;
  input = document.getElementById("myInput");
  filter = input.value.toUpperCase();
  table = document.getElementById("myTable");
  tr = table.getElementsByTagName("tr");

  arr = [];

  // Loop through all table rows, and hide those who don't match the search query
  for (i = 0; i < tr.length; i++) {
    arr.push(false);

    for (j = 0; j < tr[0].childElementCount; j++) {
      item = tr[i].getElementsByTagName("td")[j];

      if (item) {
        itemValue = item.textContent || item.innerText;

        arr[i] = arr[i] || itemValue.toUpperCase().indexOf(filter) > -1;
      }
    }
  }

  for (i = 0; i < tr.length; i++) {
    if (arr[i]) {
      tr[i].style.display = "";
    } else {
      tr[i].style.display = "none";
    }
  }
}
