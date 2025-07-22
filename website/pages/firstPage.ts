
let confettiContainer = document.getElementById("confetti-container") as HTMLDivElement;
let img = document.getElementById("imgCat")as HTMLImageElement;

 if (img) {
        img.addEventListener('click', () => {
     triggerConfettiEffect();
  });
}

 
function triggerConfettiEffect() {
  let i = 0;
  while (i < 40) {
    let confetti = document.createElement("div");
    confetti.classList.add("confetti");

    let colors = ["red", "blue", "green", "yellow", "purple", "orange"];
    let colorIndex = Math.floor(Math.random() * colors.length);
    confetti.style.backgroundColor = colors[colorIndex];

    let left = Math.random() * window.innerWidth;
    confetti.style.left = left + "px";

    let size = 6 + Math.random() * 12;
    confetti.style.width = size + "px";
    confetti.style.height = size + "px";

    confettiContainer.appendChild(confetti);

    setTimeout(function () {
      if (confettiContainer.contains(confetti)) {
        confettiContainer.removeChild(confetti);
      }
    }, 2000);

    i++;
  }
}
