function askBeforeLeave(e) {
  e.preventDefault();
  e.returnValue = '';
}

export function enablePreventLeave() {
  window.addEventListener('beforeunload', askBeforeLeave);
}

export function disablePreventLeave() {
  window.removeEventListener('beforeunload', askBeforeLeave)
}
