 let box = document.querySelector(".box") as HTMLDivElement;
 let box3 = document.querySelector(".box3") as HTMLDivElement;
 let giftbut= document.getElementById("giftbut")as HTMLButtonElement;
 giftbut.onclick=function(){
 box.classList.add('box2');
 box3.classList.add('box2');
  box.addEventListener('transitionend', () => {
    setTimeout(() => {
      window.location.href = "bookStart.html";
    }, 1000);
  }, { once: true });
   box3.addEventListener('transitionend', () => {
    setTimeout(() => {
      window.location.href = "bookStart.html";
    }, 1000);
  }, { once: true });
 
}