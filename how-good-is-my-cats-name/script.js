const input = document.getElementById("nameInput");
const output = document.getElementById("resultOutput");
const btn = document.getElementById("btn");

btn.addEventListener("click", (event) => checkLetters());

document.getElementById("nameInput").addEventListener("keyup", function (e) {
  if (e.code) {
    document.getElementById("btn").click();
  }
});

function checkLetters() {
  const inputTxt = input.value;

  if (/[i|y]+/gi.test(inputTxt)) {
    output.innerText =
      "Perfect! Your kitten's name is awesome and easily recognizable. For a better experience, remember to pronounce it with affection.";
  } else if (/[a|e]+/gi.test(inputTxt)) {
    output.innerText =
      "Good! Your kitten's name is great, and it is easy to recognize. Do not forget to reinforce the high-pitched syllables.";
  } else {
    output.innerText =
      "Not so good! Your kitten's name is lovely, but it may be difficult to recognize. Don't worry, you should keep this name and follow some of the tips below.";
  }
}
