let gallery = document.getElementById('galle')
let imgs = gallery.querySelectorAll('img')
const contentido = document.getElementById("content")
let draggin = false, mouseLocation, galleryLocation, hasMoved = false;
const images = gallery.querySelectorAll('img');
images.forEach(img => {
    img.draggable = false;
});

const imaginatios = [
    {
        year: 2014,
        title: "Imagiantio I",
        description: "Imaginatio 2014-1 it's the first instance of the Imaginatio Event done in the UMNG in the Campus Venue.<br>The topic is engineering in it of itself and with talks about video production and rendering.",
    },
    {
        year: 2014,
        title: "Imaginatio II",
        description: "Imaginatio 2014-2 is the second apereance of the event.<br>This instance is focused on a more artistic side of things with talks about 3D modeling and animation with some series being put as examples."
    },
    {
        year: 2015,
        title: "Imaginatio III",
        description: "The Imaginatio of 2015-1 was the third instance of this event.<br>This one was directed in a more artistic aspect, wiht focus in animtaion and animated series, with talks about concepts of animtaion and workshops of that same topic."
    },
    {
        year: 2015,
        title: "Imaginatio IV",
        description: "The fourth Imaginatio, the on of 2015-2, was a more open one, with topics like coding being brought up and concept art to 3D rendered having their own talks."
    },
    {
        year: 2016,
        title: "Imaginatio V",
        description: "The first Imaginatio of 2016, the fifth in general, it brought more serious topics such as the industry and profesionalism. It's also the first instance of the Geek Race."
    },
    {
        year: 2016,
        title: "Imaginatio VI",
        description: "In the second Imaginatio of 2016 it's a special one, it has a colaboration between the UMNG and the Buenaventura Univertity.<br>With talks from exstudents and other profesionals from the industry."
    },
    {
        year: 2017,
        title: "Imaginatio VII",
        description: "Imaginatio number VII, the first one of 2017.<br>It had a main focus on 3D modeling with a workshop of making character concepts and making them 3D. The results are visible in the Google Sites section of this Imaginatio."
    },
    {
        year: 2018,
        title: "Imaginatio IX",
        description: 'The Ninth Imaginatio, it brings up a more ecological topic.<br>With talks about how this engineering career can help in aspects like food production.<br>Also the first football tournament of the event.'
    },
    {
        year: 2018,
        title: "Imaginatio X",
        description: 'Imaginatio number 10 was the last instance of bi-yearly Imagination, but it was still bringing some interesting topics into the mix.<br>With ideas like monetization of work and networking. Also new videogame tournaments like LoL and Fortnite.'
    },
    {
        year: 2019,
        title: "Imaginatio XI",
        description: "Imaginatio XI goes back to some basic topics in our career, such as Design and Photograpy.<br>With not only a competition of photography that gives us a treat to the eyes but also a Certification of High Quality Education by the Governemnt,"
    },
    {
        year: 2020,
        title: "Imaginatio XII",
        description: "The 11th Imaginatio was given a special title, The Day of Multimedia Engineering.<br>A wide variety of talks from all the aspects that make this career unique are brought up, from coding to art, from practical to theorical."
    },
    {
        year: 2022,
        title: "Imaginatio XIV",
        description: "In the Imaginatio of 2022, the 14th, we go into an upcoming topic, Metaverses and diferent forms of virtual/augmented reality.<br>Drones, NFt and multiple works done by students were presented, and all was closed up by a concert of The Black Rope."
    }
]

function show(n){
    const contentImg = contentido.querySelector('img');
    const contentH1 = contentido.querySelector('h1');
    const contentP = contentido.querySelector('p');
    
    if (contentido.style.display === 'flex') {
        contentImg.style.opacity = '0';
        contentH1.style.opacity = '0';
        contentP.style.opacity = '0';
        
        setTimeout(() => {
            contentH1.textContent = imaginatios[n].title;
            contentP.innerHTML = imaginatios[n].description;
            contentImg.src = imgs[n].src;
            
            contentImg.style.opacity = '1';
            contentH1.style.opacity = '1';
            contentP.style.opacity = '1';
        }, 300);
    } else {
        gallery.style.transition = 'height 0.3s';
        gallery.style.height = '50vh';
        contentido.style.display = "flex";
        
        imgs.forEach(img => {
            img.style.height = '350px';
        });
        
        contentH1.textContent = imaginatios[n].title;
        contentP.innerHTML = imaginatios[n].description;
        contentImg.src = imgs[n].src;
        
        contentImg.style.opacity = '0';
        contentH1.style.opacity = '0';
        contentP.style.opacity = '0';
        
        setTimeout(() => {
            contentImg.style.opacity = '1';
            contentH1.style.opacity = '1';
            contentP.style.opacity = '1';
        }, 10);
    }
}

const dragStart = e => {
    draggin = true;
    hasMoved = false;
    mouseLocation = e.pageX;
    galleryLocation = gallery.scrollLeft;
}

const dragActive = e => {
    if (!draggin) return;
    
    let offset = e.pageX - mouseLocation;
    
    if (Math.abs(offset) > 5) { 
        hasMoved = true;
        e.preventDefault();
        gallery.style.pointerEvents = 'none';
    }
    
    gallery.scrollLeft = galleryLocation - offset;
}

const dragStop = e => {
    gallery.style.pointerEvents = 'auto';
    draggin = false;
    
    if (!hasMoved) {
    }
}

gallery.addEventListener('mousedown', dragStart);
gallery.addEventListener('mousemove', dragActive);
gallery.addEventListener('mouseup', dragStop);
gallery.addEventListener('mouseleave', dragStop);

images.forEach((img, n) => {
    img.addEventListener('click', (e) => {
        if (!hasMoved) {
            show(n);
        }
    });
});

document.addEventListener('click', (e) => {
    if (contentido.style.display === 'flex' && 
        !contentido.contains(e.target) && 
        !gallery.contains(e.target)) {
        contentido.style.display = 'none';
        imgs.forEach(img => {
            img.style.height = '';
        });
        gallery.style.height = '';
    }
});