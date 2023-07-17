import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { Product } from '../models/Product';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { ErrorHandlingService } from '../../../shared/services/errorhandlingservice';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient, private _errorHandlingService: ErrorHandlingService) { }
  getVat(): Observable<number> {
    return this.http.get<number>(`Product/Vat`).pipe(
      catchError(this._errorHandlingService.handleHttpError)
    );
  }
  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`Product`).pipe(
      catchError(this._errorHandlingService.handleHttpError)
    );
  }
  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`Product/${id}`);
  }
  addProduct(product: Product): Observable<{}> {
    return this.http.post<{}>(`Product`, product);
  }
  updateProduct(product: Product) : Observable<{}> {
    return this.http.put<{}>(`Product`, product);
  }
  deleteProduct(id: number) : Observable<{}> {
    return this.http.delete<Product>(`Product/${id}`);
  }



}
