import { Component } from '@angular/core';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent {
  contactMethods = [
    {
      title: 'Customer Service',
      icon: 'bi bi-headset',
      detail: '18005690',
      hours: '24/7 Support'
    },
    {
      title: 'Email Support',
      icon: 'bi bi-envelope',
      detail: 'support@infinitybank.com',
      hours: 'Response within 24 hours'
    },
    {
      title: 'Corporate Office',
      icon: 'bi bi-building',
      detail: '21 Shanku Marg, Freeganj Ujjain',
      hours: 'Mon-Fri: 9AM-5PM'
    }
  ];

  locations = [
    {
      name: 'Main Branch - Ujjain',
      address: '21 Shanku Marg, Freeganj Ujjain',
      phone: '+91 9826401234',
      hours: 'Mon-Fri: 9:00 AM - 5:00 PM\nSat: 10:00 AM - 2:00 PM',
      services: ['Personal Banking', 'Business Banking', 'Mortgage Services', 'Investment Advisory']
    },
    {
      name: 'Downtown Branch',
      address: '456 Financial Ave, New York, NY 10002',
      phone: '(212) 555-0456',
      hours: 'Mon-Fri: 9:00 AM - 6:00 PM\nSat: 10:00 AM - 3:00 PM',
      services: ['Personal Banking', 'ATM Services', 'Safe Deposit Boxes', 'Loan Services']
    },
    {
      name: 'Midtown Branch',
      address: '789 Banking Plaza, California, NY 19873',
      phone: '(212) 555-0789',
      hours: 'Mon-Fri: 8:30 AM - 5:30 PM\nSat: 9:00 AM - 1:00 PM',
      services: ['Wealth Management', 'Business Banking', 'International Services', 'Private Banking']
    }
  ];
}
