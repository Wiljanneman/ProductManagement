import { Component } from '@angular/core';

@Component({
  selector: 'app-payment-details',
  templateUrl: './payment-details.component.html',
  styleUrls: ['./payment-details.component.scss']
})
export class PaymentDetailsComponent {
  product = {
    name: 'Sample Product',
    price: 29.99,
    quantity: 2,
    date: new Date(),
  };
}
