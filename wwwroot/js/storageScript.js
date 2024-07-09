//sauvegarder
function saveToLocalStorage(key, value) {
    localStorage.setItem(key, value);
}

//récupérer
function getFromLocalStorage(key) {
    return localStorage.getItem(key);
}

//supprimer
function removeFromLocalStorage(key) {
    localStorage.removeItem(key);
}
