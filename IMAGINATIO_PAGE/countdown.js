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
