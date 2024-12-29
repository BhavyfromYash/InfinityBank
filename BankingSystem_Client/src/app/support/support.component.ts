import { Component } from '@angular/core';

@Component({
  selector: 'app-support',
  templateUrl: './support.component.html',
  styleUrls: ['./support.component.css']
})
export class SupportComponent {
  contactMethods = [
    {
      title: '24/7 Phone Support',
      icon: 'bi bi-telephone-fill',
      detail: '1-800-123-4567',
      description: 'Available round the clock for urgent assistance'
    },
    {
      title: 'Email Support',
      icon: 'bi bi-envelope-fill',
      detail: 'support@securebank.com',
      description: 'Response within 24 hours'
    },
    {
      title: 'Live Chat',
      icon: 'bi bi-chat-dots-fill',
      detail: 'Available 9AM - 6PM',
      description: 'Instant support from our team'
    }
  ];

  faqCategories = [
    {
      title: 'Online Banking',
      icon: 'bi bi-laptop',
      faqs: [
        'How do I reset my password?',
        'Is online banking secure?',
        'How to set up bill pay?'
      ]
    },
    {
      title: 'Account Services',
      icon: 'bi bi-wallet2',
      faqs: [
        'How to open a new account?',
        'What are the account fees?',
        'How to order checks?'
      ]
    },
    {
      title: 'Mobile Banking',
      icon: 'bi bi-phone',
      faqs: [
        'How to download the app?',
        'How to deposit checks?',
        'Mobile payment setup'
      ]
    }
  ];
}
