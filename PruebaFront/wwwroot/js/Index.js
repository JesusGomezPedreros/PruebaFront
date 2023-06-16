
    $(document).ready(function () {
        $('tr[data-bs-toggle="modal"]').click(function () {
            var estudianteId = $(this).data('bs-estudiante-id');
            $('#modalAgregarCurso').data('estudiante-id', estudianteId);
            cargarCursosEstudiante(estudianteId);
        });
    $('#modalAgregarCurso button.btn-primary').click(function () {
                var estudianteId = $('#modalAgregarCurso').data('estudiante-id');
    var idModulo = $('#selectModulo').val();
    var idClase = $('#selectCurso').val();

    registrarClaseEstudiante(estudianteId, idModulo, idClase);
            });
        });

    function cargarCursosEstudiante(estudianteId) {
        // Realiza una llamada AJAX para obtener los cursos del estudiante por su ID
        // y reemplaza los datos en la tabla de cursos
        $.ajax({
            url: '/Home/ConsultaCursos',
            type: 'GET',
            data: { estudianteId: estudianteId },
            success: function (data) {
                var cursosEstudianteBody = $('#cursosEstudianteBody');
                cursosEstudianteBody.empty();

                $.each(data, function (index, curso) {
                    if (curso.nombreModulo !== null && curso.nombreClase !== null) {
                        var row = $('<tr></tr>');
                        row.append('<td>' + curso.nombreModulo + '</td>');
                        row.append('<td>' + curso.nombreClase + '</td>');
                        cursosEstudianteBody.append(row);
                    }
                });
            },
            error: function (error) {
                console.log(error);
            }
        });
        }
    function registrarClaseEstudiante(estudianteId, idModulo, idClase) {
            var registrarUnion = {
        idClase: idClase,
    idModulo: idModulo,
    idEstudiante: estudianteId
            };
    $.ajax({
        url: '/Home/RegistrarClaseEstudiante',
    type: 'POST',
    data: registrarUnion,
    success: function (data) {
                    // Realizar acciones adicionales después de registrar la clase del estudiante
                    if (data === 0) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Curso ya asignado selección incorrecta!',

        })

    } else {
        Swal.fire({
            position: 'center',
            icon: 'Guardado',
            title: 'Guardado Exitoso',
            showConfirmButton: false,
            timer: 1500
        })
                        window.location.replace("/Home/Index");
                    }
                },
    error: function (error) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Error!',

        })
    }
            });
        }
