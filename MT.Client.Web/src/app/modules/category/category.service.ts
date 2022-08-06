import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { ApiResponse } from 'src/app/common/models/api-response.model';
import { BaseService } from 'src/app/common/services/base.service';
import { Category } from './models/category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private baseUrl: string = `${environment.ApiUrl}/api/categories`;

  constructor(
    private _baseService: BaseService
  ) { }

  GetAll(): Observable<Category[] | null> {
    return this._baseService.Get<Category[]>(this.baseUrl);
  }
}
