import { HttpErrorResponse } from '@angular/common/http';

export function isForbiddenHttpResponse(httpError): boolean {
  return httpError.status === 403
}
export function isNotFoundHttpResponse(httpError): boolean {
  return httpError.status === 404
}

export function isServerErrorResponse(httpError): boolean {
  return httpError.status === 500
}

export function isOfflineHttpResponse(httpError): boolean {
  return !navigator.onLine && httpError.status === 0
}

export function isBadRequestHttpResponse(httpError): boolean {
  return httpError.status === 400
}
