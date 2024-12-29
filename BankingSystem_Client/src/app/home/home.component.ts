import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  stats = [
    { number: '2M+', label: 'Active Users' },
    { number: '$50B+', label: 'Transactions' },
    { number: '150+', label: 'Countries' },
    { number: '24/7', label: 'Support' }
  ];

  features = [
    {
      title: 'Secure Banking',
      description: 'Bank with confidence using our state-of-the-art security systems',
      icon: 'bi bi-shield-check fs-1'
    },
    {
      title: 'Mobile Banking',
      description: 'Access your accounts anytime, anywhere with our mobile app',
      icon: 'bi bi-phone fs-1'
    },
    {
      title: 'Quick Transfers',
      description: 'Send money instantly to anyone, anywhere in the world',
      icon: 'bi bi-arrow-left-right fs-1'
    }
  ];
}