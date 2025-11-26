// perfil.js

// Datos simulados del historial de compras
const historialData = [
  { curso: "Desarrollo Web desde Cero", categoria: "Programación / Web", fecha: "05/01/2025", metodoPago: "Tarjeta", instructor: "Carlos DevMaster" },
  { curso: "Excel para Principiantes 2025", categoria: "Productividad / Office", fecha: "08/01/2025", metodoPago: "PayPal", instructor: "Laura DataCoach" },
  { curso: "Fotografía Profesional con Smartphone", categoria: "Arte / Fotografía", fecha: "10/01/2025", metodoPago: "Apple Pay", instructor: "Miguel LensStudio" },
  { curso: "Marketing Digital Completo", categoria: "Negocios / Marketing", fecha: "12/01/2025", metodoPago: "Tarjeta", instructor: "Ana GrowthMentor" },
  { curso: "Edición de Video con Premiere Pro", categoria: "Multimedia / Video", fecha: "15/01/2025", metodoPago: "Google Pay", instructor: "Javier FilmPro" }
];

// Cargar datos del usuario desde localStorage
function loadUserData() {
  const userData = JSON.parse(localStorage.getItem('userData'));
  if (!userData) {
    window.location.href = 'iniciodecesion.html';
    return;
  }

  // Datos del usuario (simulados, pero puedes cambiarlos)
  const user = {
    nombre: "DANILLO JOSUE NEGRETE RIVERA",
    email: "danilo@gmail.com",
    rol: "estudiante universitario",
    telefono: "09594867458",
    avatar: "../img/default-avatar.png"
  };

  // Actualizar ambos elementos (el de la barra lateral y el de la tabla)
  document.getElementById('userAvatar').src = user.avatar || '../img/default-avatar.png';
  document.getElementById('userName').textContent = user.nombre || 'Usuario';
  document.getElementById('userEmail').textContent = user.email || 'correo@example.com';
  document.getElementById('userRol').textContent = user.rol || 'Rol no definido';
  document.getElementById('userPhone').textContent = `Teléfono: ${user.telefono || 'No disponible'}`;

  // Actualizar también en la tabla (si existe)
  const userAvatarTable = document.getElementById('userAvatarTable');
  const userNameTable = document.getElementById('userNameTable');
  const userEmailTable = document.getElementById('userEmailTable');
  const userRolTable = document.getElementById('userRolTable');
  const userPhoneTable = document.getElementById('userPhoneTable');

  if (userAvatarTable) userAvatarTable.src = user.avatar || '../img/default-avatar.png';
  if (userNameTable) userNameTable.textContent = user.nombre || 'Usuario';
  if (userEmailTable) userEmailTable.textContent = user.email || 'correo@example.com';
  if (userRolTable) userRolTable.textContent = user.rol || 'Rol no definido';
  if (userPhoneTable) userPhoneTable.textContent = `Teléfono: ${user.telefono || 'No disponible'}`;
}

// Renderizar la tabla de historial
function renderHistorial(data) {
  const tbody = document.getElementById('historialTableBody');
  tbody.innerHTML = '';

  data.forEach(item => {
    const row = document.createElement('tr');
    row.innerHTML = `
      <td>${item.curso}</td>
      <td>${item.categoria}</td>
      <td>${item.fecha}</td>
      <td>${item.metodoPago}</td>
      <td>${item.instructor}</td>
    `;
    tbody.appendChild(row);
  });
}

// Aplicar filtros
function applyFilters() {
  const curso = document.getElementById('filterCurso').value.toLowerCase();
  const categoria = document.getElementById('filterCategoria').value.toLowerCase();
  const fecha = document.getElementById('filterFecha').value;
  const metodoPago = document.getElementById('filterMetodoPago').value.toLowerCase();
  const docente = document.getElementById('filterDocente').value.toLowerCase();
  const buscar = document.getElementById('filterBuscar').value.toLowerCase();

  const filtered = historialData.filter(item => {
    return (!curso || item.curso.toLowerCase().includes(curso)) &&
           (!categoria || item.categoria.toLowerCase().includes(categoria)) &&
           (!fecha || item.fecha === fecha) &&
           (!metodoPago || item.metodoPago.toLowerCase().includes(metodoPago)) &&
           (!docente || item.instructor.toLowerCase().includes(docente)) &&
           (!buscar || 
             item.curso.toLowerCase().includes(buscar) ||
             item.categoria.toLowerCase().includes(buscar) ||
             item.instructor.toLowerCase().includes(buscar));
  });

  renderHistorial(filtered);
}

// Limpiar filtros