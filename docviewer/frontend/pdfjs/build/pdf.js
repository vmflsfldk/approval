(function () {
  const CDN_VERSION = '3.7.107';
  if (!window._pdfjsReadyPromise) {
    window._pdfjsReadyPromise = new Promise((resolve, reject) => {
      const script = document.createElement('script');
      script.src = `https://cdnjs.cloudflare.com/ajax/libs/pdf.js/${CDN_VERSION}/pdf.min.js`;
      script.onload = () => resolve(window.pdfjsLib);
      script.onerror = () => reject(new Error('Failed to load pdf.js library'));
      document.head.appendChild(script);
    });
  }
})();
