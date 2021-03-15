import { Observable, ObservableInput, OperatorFunction, ObservedValueOf, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { throwUnless } from '../error-handling/guards';

export function matchError<T, O extends ObservableInput<any>>(isMatchingError: (any) => boolean, selector: (err: any, caught: Observable<T>) => O): OperatorFunction < T, T | ObservedValueOf <O>> {
  return catchError((err, caught) => {
    if (isMatchingError(err)) {
      return selector(err, caught)
    }
    return throwError(err)
  })
}
