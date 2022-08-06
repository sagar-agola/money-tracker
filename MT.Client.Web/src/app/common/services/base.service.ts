import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ApiResponse } from '../models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  constructor(
    private _http: HttpClient,
    private _toastr: ToastrService
  ) { }

  Get<T>(method: string): Observable<T | null> {
    return this._http.get<ApiResponse<T>>(method, {
      headers: this.setHeaders()
    }).pipe(
      map((response) => this.extractData<T>(response)),
      catchError(err => this.handleError(err))
    );
  }

  private setHeaders(contentType?: string): HttpHeaders {
    let headers = new HttpHeaders();

    if (contentType === 'formData') { }
    else if (contentType) {
      headers = headers.set('Content-Type', contentType);
    } else {
      headers = headers.set('Content-Type', 'application/json');
    }

    return headers;
  }

  private extractData<T>(response: ApiResponse<T>): T | null {
    this.showMessages(response);

    if (response && response.statusCode == 200) {
      return response.data;
    }

    return null;
  }

  private handleError(response: Response | any): Observable<null> {
    const error: any = response.error;

    this.showMessages(error);

    return of(null);
  }

  private showMessages(response: any): void {
    if (response && response.messages && response.messages.length > 0) {
      response.messages.forEach((message: string) => {
        if (response.statusCode == 200) {
          this._toastr.info(message);
        }
        else {
          this._toastr.error(message);
        }
      });
    }
  }
}
