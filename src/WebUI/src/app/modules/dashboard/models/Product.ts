export interface Product {
  id: number;
  name: string;
  description: string;
  regularPrice: number;
  salePrice: number;
  isOnSale: boolean;
  quantity: number;
  vATAmount: number;
  includingVATAmount: number;
  excludingVATAmount: number;
}