// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Zoekfunctionaliteit
document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.getElementById('searchInput');
    const searchResults = document.getElementById('searchResults');
    const searchButton = document.getElementById('searchButton');
    let debounceTimer;

    if (searchInput && searchResults) {
        // Verwerk input wijzigingen met debounce
        searchInput.addEventListener('input', function () {
            clearTimeout(debounceTimer);
            debounceTimer = setTimeout(() => performSearch(), 300);
        });

        // Verwerk zoekknop klik
        searchButton?.addEventListener('click', function () {
            performSearch();
        });

        // Sluit resultaten wanneer er buiten wordt geklikt
        document.addEventListener('click', function (e) {
            if (!searchInput.contains(e.target) && !searchResults.contains(e.target)) {
                searchResults.classList.add('d-none');
            }
        });

        // Toon resultaten wanneer de input focus krijgt
        searchInput.addEventListener('focus', function () {
            if (searchResults.children[0].children.length > 0) {
                searchResults.classList.remove('d-none');
            }
        });
    }

    function performSearch() {
        const query = searchInput.value.trim();
        if (!query) {
            searchResults.classList.add('d-none');
            return;
        }

        const searchUrl = searchInput.dataset.searchUrl;
        fetch(`${searchUrl}?query=${encodeURIComponent(query)}`)
            .then(response => response.json())
            .then(data => {
                const resultsContainer = searchResults.querySelector('.list-group');
                resultsContainer.innerHTML = '';

                if (data.results.length === 0) {
                    resultsContainer.innerHTML = '<div class="list-group-item">Geen resultaten gevonden</div>';
                } else {
                    data.results.forEach(result => {
                        const item = document.createElement('a');
                        item.href = result.url;
                        item.className = 'list-group-item list-group-item-action search-result-item';
                        item.innerHTML = `
                            <div>${result.name}</div>
                            <span class="search-result-type">${result.type}</span>
                        `;
                        resultsContainer.appendChild(item);
                    });
                }

                searchResults.classList.remove('d-none');
            })
            .catch(error => {
                console.error('Fout bij het uitvoeren van de zoekopdracht:', error);
                searchResults.classList.add('d-none');
            });
    }
});
