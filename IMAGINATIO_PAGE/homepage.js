document.addEventListener('DOMContentLoaded', () => {
  const carousel = document.querySelector('.carousel_imaginatio');
  if (!carousel) return;

  const slideContainer = carousel.querySelector('.carouselgen');
  const slides = Array.from(carousel.querySelectorAll('.slide'));
  const btnNext = carousel.querySelector('.next');
  const btnPrev = carousel.querySelector('.prev');

  let autoplayInterval;
  const delay = 5000; // tiempo entre slides (ms)

  function next() {
    slideContainer.appendChild(slides[0]);
    slides.push(slides.shift());
  }

  function prev() {
    slideContainer.prepend(slides[slides.length - 1]);
    slides.unshift(slides.pop());
  }

  function startAutoplay() {
    autoplayInterval = setInterval(next, delay);
  }

  function stopAutoplay() {
    clearInterval(autoplayInterval);
  }

  // Click en botones
  if (btnNext) btnNext.addEventListener('click', () => { next(); resetAutoplay(); });
  if (btnPrev) btnPrev.addEventListener('click', () => { prev(); resetAutoplay(); });

  function resetAutoplay() {
    stopAutoplay();
    startAutoplay();
  }

  // Click en cualquier slide
  slides.forEach((slide, index) => {
    slide.addEventListener('click', () => {
      let targetIndex = slides.indexOf(slide);

      // Queremos que el clicado quede en la posición central -> index 1
      while (slides[1] !== slide) {
        if (targetIndex > 1) {
          next();
        } else {
          prev();
        }
        targetIndex = slides.indexOf(slide); // actualizar después de rotar
      }
      resetAutoplay();
    });
  });

  // autoplay con pausa al pasar el mouse
  carousel.addEventListener('mouseenter', stopAutoplay);
  carousel.addEventListener('mouseleave', startAutoplay);

  // iniciar autoplay
  startAutoplay();
});





//Carrusel 2
document.querySelectorAll(".carousel").forEach(carousel => {
  const track = carousel.querySelector(".carousel-track");
  const slides = Array.from(track.children);

  // Crear indicador dinámico
  const progress = document.createElement("div");
  progress.classList.add("carousel-progress");
  slides.forEach(() => {
    let dot = document.createElement("span");
    progress.appendChild(dot);
  });
  carousel.appendChild(progress);

  const dots = Array.from(progress.children);

  let currentIndex = 0;
  let startX = 0;
  let currentOffset = 0;
  let dragging = false;

  function updateSlider() {
    const offset = -currentIndex * carousel.offsetWidth + currentOffset;
    track.style.transform = `translateX(${offset}px)`;

    // actualizar clases "active"
    slides.forEach((s, i) => s.classList.toggle("active", i === currentIndex));
    dots.forEach((d, i) => d.classList.toggle("active", i === currentIndex));
  }

  function goToSlide(index) {
    currentIndex = Math.max(0, Math.min(index, slides.length - 1));
    currentOffset = 0;
    updateSlider();
  }

  // mouse
  carousel.addEventListener("mousedown", e => {
    dragging = true;
    startX = e.clientX;
    track.style.transition = "none";
  });

  window.addEventListener("mouseup", () => {
    if (!dragging) return;
    dragging = false;
    track.style.transition = "transform 0.35s cubic-bezier(0.25, 1, 0.5, 1)";

    if (currentOffset < -carousel.offsetWidth / 4 && currentIndex < slides.length - 1) {
      currentIndex++;
    } else if (currentOffset > carousel.offsetWidth / 4 && currentIndex > 0) {
      currentIndex--;
    }
    currentOffset = 0;
    updateSlider();
  });

  window.addEventListener("mousemove", e => {
    if (!dragging) return;
    let dx = e.clientX - startX;
    startX = e.clientX;
    currentOffset += dx;
    updateSlider();
  });

  // touch
  carousel.addEventListener("touchstart", e => {
    dragging = true;
    startX = e.touches[0].clientX;
    track.style.transition = "none";
  });

  window.addEventListener("touchend", () => {
    if (!dragging) return;
    dragging = false;
    track.style.transition = "transform 0.35s cubic-bezier(0.25, 1, 0.5, 1)";

    if (currentOffset < -carousel.offsetWidth / 4 && currentIndex < slides.length - 1) {
      currentIndex++;
    } else if (currentOffset > carousel.offsetWidth / 4 && currentIndex > 0) {
      currentIndex--;
    }
    currentOffset = 0;
    updateSlider();
  });

  window.addEventListener("touchmove", e => {
    if (!dragging) return;
    let cx = e.touches[0].clientX;
    let dx = cx - startX;
    startX = cx;
    currentOffset += dx;
    updateSlider();
  });

  // click en dots
  dots.forEach((d, i) => {
    d.addEventListener("click", () => goToSlide(i));
  });

  // inicial
  updateSlider();
});
