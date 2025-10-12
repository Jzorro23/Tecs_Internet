document.addEventListener("DOMContentLoaded", () => {

  const container = document.querySelector('.split-container');
  const actionButtons = Array.from(document.querySelectorAll('.action-btn'));
  const panels = Array.from(document.querySelectorAll('.panel'));
  const closeButtons = Array.from(document.querySelectorAll('.close-btn'));

  function openPanel(targetId, triggeringBtn) {
    const panel = document.getElementById(targetId);
    if (!panel) return;

    // marcar estado UI
    container.classList.add('expanded');
    panel.classList.add('active');
    panel.setAttribute('aria-hidden', 'false');

    // ocultar botones y marcar aria
    actionButtons.forEach(btn => {
      btn.setAttribute('aria-expanded', 'true');
      btn.style.pointerEvents = 'none';
    });

    // focus en primer elemento del panel
    const focusable = panel.querySelector('button, a, [tabindex]');
    if(focusable) focusable.focus();
  }

  function closePanel(panel) {
    if(!panel) return;
    panel.classList.remove('active');
    panel.setAttribute('aria-hidden', 'true');

    // restaurar UI
    container.classList.remove('expanded');
    actionButtons.forEach(btn => {
      btn.setAttribute('aria-expanded', 'false');
      btn.style.pointerEvents = '';
    });

    // devolver foco al primer botón (opcional)
    const firstBtn = document.querySelector('.action-btn');
    if(firstBtn) firstBtn.focus();
  }

  // abrir con click
  actionButtons.forEach(btn => {
    btn.addEventListener('click', (e) => {
      const target = btn.dataset.target;
      openPanel(target, btn);
    });
    // soporte teclado: Enter / Space
    btn.addEventListener('keydown', (e) => {
      if (e.key === 'Enter' || e.key === ' ') {
        e.preventDefault(); btn.click();
      }
    });
  });

  // cerrar con el botón X
  closeButtons.forEach(cb => {
    cb.addEventListener('click', (e) => {
      const panel = cb.closest('.panel');
      closePanel(panel);
    });
  });

  // cerrar con ESC
  document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape') {
      const active = document.querySelector('.panel.active');
      if (active) closePanel(active);
    }
  });

  // (Opcional) si haces click en el panel fuera del contenido puedes cerrarlo:
  panels.forEach(p => {
    p.addEventListener('click', (evt) => {
      if (evt.target === p) { // click directo en la capa del panel
        closePanel(p);
      }
    });
  });

});

//CARRUSEL GEN
document.addEventListener('DOMContentLoaded', () => {
  document.querySelectorAll('.carousel_general').forEach(carousel => {
    const slideContainer = carousel.querySelector('.carouselgen');
    const slides = Array.from(slideContainer.querySelectorAll('.slide'));
    const btnNext = carousel.querySelector('.next');
    const btnPrev = carousel.querySelector('.prev');

    let autoplayInterval;
    const delay = 4000; // tiempo entre slides (ms)

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

    function resetAutoplay() {
      stopAutoplay();
      startAutoplay();
    }

    // Botones
    if (btnNext) btnNext.addEventListener('click', () => { next(); resetAutoplay(); });
    if (btnPrev) btnPrev.addEventListener('click', () => { prev(); resetAutoplay(); });

    // Click en slide → mover al centro
    slides.forEach(slide => {
      slide.addEventListener('click', () => {
        let targetIndex = slides.indexOf(slide);
        while (slides[1] !== slide) {
          if (targetIndex > 1) {
            next();
          } else {
            prev();
          }
          targetIndex = slides.indexOf(slide);
        }
        resetAutoplay();
      });
    });

    // autoplay con pausa al hover
    carousel.addEventListener('mouseenter', stopAutoplay);
    carousel.addEventListener('mouseleave', startAutoplay);

    startAutoplay();
  });
});
