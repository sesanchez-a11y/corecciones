// script-cursos.js

document.addEventListener('DOMContentLoaded', function() {

    // 1. Funcionalidad de búsqueda
    const searchInput = document.getElementById('searchInput');
    const searchButton = document.getElementById('searchButton');
    const courseItems = document.querySelectorAll('.course-item');

    searchButton.addEventListener('click', function() {
        const term = searchInput.value.toLowerCase().trim();
        courseItems.forEach(item => {
            const title = item.querySelector('.course-item-info h5').textContent.toLowerCase();
            if (title.includes(term)) {
                item.style.display = 'flex';
            } else {
                item.style.display = 'none';
            }
        });
    });

    searchInput.addEventListener('keypress', function(e) {
        if (e.key === 'Enter') {
            searchButton.click();
        }
    });

    // 2. Funcionalidad de restablecer filtros
    document.getElementById('restablecerFiltros').addEventListener('click', function() {
        document.getElementById('ordenarPor').value = 'recientes';
        document.getElementById('filtrarPor').value = 'todas';
        searchInput.value = '';
        courseItems.forEach(item => item.style.display = 'flex');
    });

    // 3. Simular carrusel (solo para demostración)
    let currentSlide = 0;
    const slides = [
        {
            title: "Empieza a Aprender por menos",
            text: "Si eres nuevo en Edumentor, tenemos buenas noticias: por tiempo limitado, tenemos cursos desde solo 10,99 US$ para nuevos estudiantes. Compra ya.",
            img: "https://cdn-icons-png.flaticon.com/512/62/62090.png"
        },
        {
            title: "Nuevos cursos disponibles",
            text: "Tenemos más de 500 cursos nuevos en tecnología, negocios y diseño. ¡No te quedes atrás!",
            img: "https://cdn-icons-png.flaticon.com/512/2922/2922706.png"
        }
    ];

    function showSlide(index) {
        const container = document.querySelector('.carousel-container');
        const textDiv = container.querySelector('.carousel-text');
        const imgDiv = container.querySelector('.carousel-image img');
        
        textDiv.innerHTML = `
            <h2 class="h4 fw-bold mb-2">${slides[index].title}</h2>
            <p class="small mb-0">${slides[index].text}</p>
        `;
        imgDiv.src = slides[index].img;
    }

    window.nextSlide = function() {
        currentSlide = (currentSlide + 1) % slides.length;
        showSlide(currentSlide);
    };

    window.prevSlide = function() {
        currentSlide = (currentSlide - 1 + slides.length) % slides.length;
        showSlide(currentSlide);
    };

    // Iniciar con el primer slide
    showSlide(currentSlide);

    // 4. Efecto hover en WhatsApp
    const whatsappIcon = document.querySelector('.whatsapp-icon');
    if (whatsappIcon) {
        whatsappIcon.addEventListener('mouseenter', function() {
            this.style.transform = 'scale(1.1)';
            this.style.boxShadow = '0 8px 16px rgba(0,0,0,0.3)';
        });
        whatsappIcon.addEventListener('mouseleave', function() {
            this.style.transform = 'scale(1)';
            this.style.boxShadow = '0 4px 8px rgba(0,0,0,0.2)';
        });
    }

    // 5. Manejar estado activo del menú (opcional si no usas Bootstrap)
    const navLinks = document.querySelectorAll('.nav-link');
    navLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault();
            navLinks.forEach(l => l.classList.remove('active'));
            this.classList.add('active');
        });
    });
});