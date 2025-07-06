// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Cookie-Banner
function showCookieBanner() {
    if (!localStorage.getItem('cookieAccepted')) {
        document.getElementById('cookie-banner').style.display = 'block';
    }
}
function acceptCookies() {
    localStorage.setItem('cookieAccepted', 'true');
    document.getElementById('cookie-banner').style.display = 'none';
}
document.addEventListener('DOMContentLoaded', showCookieBanner);
