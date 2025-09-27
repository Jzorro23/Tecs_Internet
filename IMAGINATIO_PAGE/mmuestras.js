// Seleccionamos todos los .card6 img
const images = document.querySelectorAll(".card6 img");
const lightbox = document.getElementById("lightbox");
const lightboxImg = document.getElementById("lightbox-img");
const caption = document.getElementById("caption");
const closeBtn = document.querySelector(".close");

// Botones prev/next (añádelos al HTML del lightbox)
const prevBtn = document.getElementById("prev");
const nextBtn = document.getElementById("next");

let currentIndex = 0;

// Función para mostrar imagen en el lightbox
function showImage(index) {
  if (index < 0) index = images.length - 1;
  if (index >= images.length) index = 0;
  currentIndex = index;
  lightboxImg.src = images[currentIndex].src;
  caption.textContent = images[currentIndex].alt;
  lightbox.style.display = "flex";
}

// Al hacer clic en cada imagen
images.forEach((img, i) => {
  img.addEventListener("click", () => {
    showImage(i);
  });
});

// Navegación
prevBtn.addEventListener("click", (e) => {
  e.stopPropagation();
  showImage(currentIndex - 1);
});

nextBtn.addEventListener("click", (e) => {
  e.stopPropagation();
  showImage(currentIndex + 1);
});

// Cerrar al hacer clic en la X
closeBtn.addEventListener("click", () => {
  lightbox.style.display = "none";
});

// Cerrar si se hace clic fuera de la imagen
lightbox.addEventListener("click", (e) => {
  if (e.target === lightbox) {
    lightbox.style.display = "none";
  }
});
