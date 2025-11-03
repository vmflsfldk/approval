const CDN_VERSION = '3.7.107';
if (typeof importScripts === 'function') {
  importScripts(`https://cdnjs.cloudflare.com/ajax/libs/pdf.js/${CDN_VERSION}/pdf.worker.min.js`);
} else {
  console.error('pdf.worker.js should be loaded inside a worker context.');
}
