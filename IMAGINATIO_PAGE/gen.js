//COUNTDOWN
// Fecha objetivo → Navidad (25 diciembre, medianoche)
const targetDate = new Date("December 25, 2025 00:00:00").getTime();

// Selecciona TODOS los spans con clase real-timer
const timers = document.querySelectorAll(".real-timer");

function updateCountdown() {
  const now = new Date().getTime();
  const distance = targetDate - now;

  let fullFormat, compactFormat;

  if (distance <= 0) {
    fullFormat = compactFormat = "🎄 0d 0h 0m 0s";
  } else {
    const days = Math.floor(distance / (1000 * 60 * 60 * 24));
    const hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    const minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    const seconds = Math.floor((distance % (1000 * 60)) / 1000);

    // Dos formatos: largo y compacto
    fullFormat = `${days}d ${hours}h ${minutes}m ${seconds}s`;
    compactFormat = `${days} days`;
  }

  // Cada contador decide cómo mostrarse según el dataset
  timers.forEach(timer => {
    if (timer.dataset.format === "compact") {
      timer.textContent = compactFormat;
    } else {
      timer.textContent = fullFormat;
    }
  });
}

// Actualiza cada segundo
setInterval(updateCountdown, 1000);
updateCountdown(); // ejecución inicial inmediata

// SCROLL + Typewriter trigger (reinicio al entrar/salir)
document.addEventListener('DOMContentLoaded', () => {
  const container = document.querySelector('.main_page');
  if (!container) return;

  const items = Array.from(container.querySelectorAll('[data-animate]'));
  const typewriters = Array.from(container.querySelectorAll('.typewriter, .typewriter-multiline'));

  const rootMarginBottom = -100; // margen virtual

  function clamp(v, a=0, b=1){ return Math.min(Math.max(v,a),b); }

  function update() {
    const rect = container.getBoundingClientRect();
    const winH = window.innerHeight;
    const start = winH - rootMarginBottom; 
    const end = -rect.height; 
    const total = start - end;
    const current = start - rect.top;
    const progress = clamp(current / total, 0, 1);

    // --- animaciones de aparición (como antes)
    items.forEach((el) => {
      const elRect = el.getBoundingClientRect();
      const offsetInside = (elRect.top - rect.top) / (rect.height || 1); 
      const localProgress = clamp((progress - (offsetInside - 0.15)) / 0.3, 0, 1);

      el.style.opacity = localProgress;
      el.style.transform = `translateY(${(1 - localProgress) * 24}px)`;
    });

    // --- typewriters (añade/quita clase active según visibilidad)
    typewriters.forEach(el => {
      const elRect = el.getBoundingClientRect();
      const inView = elRect.top < winH - 80 && elRect.bottom > 0;

      if (inView) {
        // reinicia la animación eliminando y re-agregando la clase
        if (!el.classList.contains('active')) {
          el.classList.remove('active'); // por si acaso
          void el.offsetWidth;          // trigger reflow para reiniciar animación
          el.classList.add('active');
        }
      } else {
        el.classList.remove('active'); // cuando sale de la vista
      }
    });
  }

  let ticking = false;
  function onScroll() {
    if (!ticking) {
      requestAnimationFrame(() => { update(); ticking = false; });
      ticking = true;
    }
  }

  window.addEventListener('scroll', onScroll, { passive: true });
  window.addEventListener('resize', onScroll);
  update();
});


// SCROLL + subtítulos trigger (entrada/salida de viewport)
document.addEventListener("DOMContentLoaded", () => {
  const subtitulos = document.querySelectorAll(".neon-subtitulo");

  subtitulos.forEach(subtitulo => {
    const spans = subtitulo.querySelectorAll("span");

    // Asignamos índice a cada letra
    spans.forEach((span, i) => {
      span.style.setProperty("--i", i + 1);
    });

    // Función para animar entrada
    function animateIn(startProgress = 0) {
      spans.forEach((span, i) => {
        span.animate(
          [
            { transform: `translate(${(1 - startProgress) * -200}px, 0) scale(0.5)`, opacity: startProgress },
            { transform: "translate(20px,0) scale(1)", opacity: 1, offset: 0.6 },
            { transform: "translate(0,0) scale(1)", opacity: 1 }
          ],
          {
            duration: 600,
            delay: i * 80,
            fill: "forwards"
          }
        );
      });
    }

    // Observador para disparar la animación al entrar/salir
    function checkInView() {
      const rect = subtitulo.getBoundingClientRect();
      const inView = rect.top < window.innerHeight - 80 && rect.bottom > 0;

      if (inView) {
        // reinicia la animación
        animateIn();
      }
    }

    let ticking = false;
    function onScroll() {
      if (!ticking) {
        requestAnimationFrame(() => { checkInView(); ticking = false; });
        ticking = true;
      }
    }

    window.addEventListener("scroll", onScroll, { passive: true });
    window.addEventListener("resize", onScroll);

    // animación inicial si ya está en pantalla
    checkInView();
  });
});



// Generar estrellas dinámicamente
function generarEstrellas(id, cantidad, tamaño, color) {
  const el = document.getElementById(id);
  let sombras = [];
  const ancho = window.innerWidth;
  const alto = window.innerHeight;

  for (let i = 0; i < cantidad; i++) {
    const x = Math.floor(Math.random() * ancho);
    const y = Math.floor(Math.random() * alto);
    sombras.push(`${x}px ${y}px ${color}`);
  }

  el.style.width = `${tamaño}px`;
  el.style.height = `${tamaño}px`;
  el.style.boxShadow = sombras.join(', ');
}

// Generar las tres capas
function inicializarEstrellas() {
  generarEstrellas('stars', 300, 2, 'white');      // estrellas pequeñas rápidas
  generarEstrellas('stars2', 150, 4, '#ccccff');  // medianas más lentas
  generarEstrellas('stars3', 80, 6, '#aaaaff');   // grandes muy lentas
}

// Regenerar estrellas al redimensionar
window.addEventListener('resize', inicializarEstrellas);

// Inicialización al cargar
window.addEventListener('DOMContentLoaded', inicializarEstrellas);



