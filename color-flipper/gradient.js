const hex = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, "A", "B", "C", "D", "E", "F"];

const btn = document.getElementById("btn");
const color = document.querySelector(".color");

btn.addEventListener("click", function () {
  let hexColor1 = "#";
  for (let i = 0; i < 6; i++) {
    hexColor1 += hex[getRandomNumber()];
  }

  let hexColor2 = "#";
  for (let i = 0; i < 6; i++) {
    hexColor2 += hex[getRandomNumber()];
  }

  color.textContent = hexColor1 + " + " + hexColor2;
  document.body.style.backgroundImage =
    "linear-gradient(to right, " + hexColor1 + ", " + hexColor2 + ")";
});

function getRandomNumber() {
  return Math.floor(Math.random() * hex.length);
}
