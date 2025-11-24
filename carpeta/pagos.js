// script-pagos.js

document.addEventListener('DOMContentLoaded', function() {

    // 1. Validar selección de método de pago
    const paymentMethods = document.querySelectorAll('input[name="paymentMethod"]');
    const payButton = document.querySelector('.btn-primary');

    paymentMethods.forEach(radio => {
        radio.addEventListener('change', function() {
            if (this.value === 'tarjetas') {
                alert("El método de pago 'Tarjetas' aún no está disponible. Por favor, elige PayPal.");
                this.checked = false;
                document.getElementById('paypal1').checked = true;
            }
        });
    });

    // 2. Simular pago al hacer clic en el botón
    payButton.addEventListener('click', function(e) {
        e.preventDefault();
        const selectedMethod = document.querySelector('input[name="paymentMethod"]:checked').value;

        if (selectedMethod === 'paypal1' || selectedMethod === 'paypal2') {
            alert("✅ Pago procesado con éxito!\n\nGracias por comprar con EduMentor.\nTu curso estará disponible en tu perfil.");
            // Aquí podrías redirigir a una página de confirmación
            // window.location.href = 'confirmacion.html';
        } else {
            alert("Por favor, selecciona un método de pago válido.");
        }
    });

    // 3. Efecto hover en WhatsApp
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

    // 4. Cambiar texto del botón si se selecciona otro método (opcional)
    document.getElementById('paypal2').addEventListener('change', function() {
        payButton.textContent = "Pagar 10.99 US$ por Paypal (Danilo)";
    });

    document.getElementById('paypal1').addEventListener('change', function() {
        payButton.textContent = "Pagar 10.99 US$ por Paypal";
    });
});