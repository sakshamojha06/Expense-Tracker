import { HttpInterceptorFn } from "@angular/common/http";

export const ngrokInterceptor: HttpInterceptorFn = (req, next) => {
  const modifiedRequest = req.clone({
    setHeaders: {
      'ngrok-skip-browser-warning': 'true'
    }
  });

  return next(modifiedRequest);
}