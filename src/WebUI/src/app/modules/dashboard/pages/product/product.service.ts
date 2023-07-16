import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../../models/Product';
import { HttpClient, HttpRequest } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }
  getVat(): Observable<number> {
    return this.http.get<number>(`Product/Vat`);
  }
  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`Product`);
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
