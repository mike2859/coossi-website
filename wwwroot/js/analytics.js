// Suivi des conversions GA4 (envoi direct via gtag, visible dans GA4 sans config GTM)
function coossiSendEvent(name, params) {
    if (typeof gtag === 'function') {
        gtag('event', name, params || {});
    }
}

window.coossiAnalytics = {
    trackLead: function (formName) {
        coossiSendEvent('generate_lead', { form_name: formName || 'contact' });
    }
};

document.addEventListener('click', function (e) {
    var link = e.target.closest('a');
    if (!link) return;

    var href = link.getAttribute('href') || '';

    if (href.startsWith('tel:')) {
        coossiSendEvent('click_to_call', { phone_number: href.slice(4) });
    } else if (href.startsWith('mailto:')) {
        coossiSendEvent('click_to_email', { email_address: href.slice(7) });
    } else if (link.classList.contains('google-review-badge')) {
        coossiSendEvent('google_review_click', {});
    }
});
