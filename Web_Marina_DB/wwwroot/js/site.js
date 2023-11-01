function LogikBtnVerwerfen() {
    document.getElementById('neuesBild').value = '';
    // Vorschau-Bild entfernen, falls das Bild verworfen wird
    document.getElementById('Vorschau').removeAttribute('src');

    // Button "Verwerfen" ausblenden, wenn die Option Verwerfen gewählt wurde
    document.getElementById('Btn_Verwerfen').style.visibility = 'hidden';
}


function SicherheitsabfrageVorLöschen() {
    var deleteLinks = document.querySelectorAll('#delete-link');
    for (var i = 0; i < deleteLinks.length; i++) {
        deleteLinks[i].addEventListener('click', function (event) {
            event.preventDefault();

            var choice = confirm('Möchten Sie den ausgewählten Datensatz wirklich löschen?');
            // var choice = confirm(this.title + '?' => boot.Name löschen? (title: siehe oben);
            if (choice) {
                window.location.href = this.href;
            }
        });
    }
}

// Aus der lightbox zurück auf die vorherige Seite per ESC oder Backspace
document.addEventListener('keydown', function (event) {
    if (event.key === 'Escape' || event.key === 'Backspace') {
        var lightbox = document.querySelector('.lightbox:target');
        if (lightbox) {
            history.back();
        }
    }
});
