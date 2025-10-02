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

