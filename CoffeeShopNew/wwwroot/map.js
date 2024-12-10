function initMap(coordinates) {
    const map = L.map('map').setView([51.505, -0.09], 13); // Default center

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '© OpenStreetMap contributors'
    }).addTo(map);

    // Add markers for each address
    Object.keys(coordinates).forEach(address => {
        const { Latitude, Longitude } = coordinates[address];
        L.marker([Latitude, Longitude])
            .addTo(map)
            .bindPopup(`<b>${address}</b>`);
    });
}
