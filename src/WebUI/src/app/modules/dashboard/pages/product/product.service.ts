import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../../models/Product';
import { HttpClient, HttpRequest } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

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
  deleteProduct(product: Product) : Observable<{}> {
    const req = new HttpRequest('DELETE', 'Product', product);
    return this.http.request<{}>(req);
  }



}
