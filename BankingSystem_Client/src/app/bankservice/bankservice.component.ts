import { Component } from '@angular/core';

@Component({
  selector: 'app-bankservice',
  templateUrl: './bankservice.component.html',
  styleUrls: ['./bankservice.component.css']
})
export class BankserviceComponent {
  mainServices = [
    {
      title: 'Personal Banking',
      icon: 'bi bi-person-circle',
      description: 'Tailored banking solutions for individuals',
      features: ['Savings Account', 'Checking Account', 'Fixed Deposits', 'Personal Loans']
    },
    {
      title: 'Business Banking',
      icon: 'bi bi-briefcase',
      description: 'Comprehensive solutions for your business needs',
      features: ['Business Accounts', 'Merchant Services', 'Trade Finance', 'Business Loans']
    },
    {
      title: 'Digital Banking',
      icon: 'bi bi-phone',
      description: 'Bank anytime, anywhere with our digital services',
      features: ['Mobile Banking', 'Online Banking', 'Digital Wallet', 'E-Statements']
    }
  ];

  specializedServices = [
    {
      title: 'Investment Services',
      icon: 'bi bi-graph-up-arrow',
      description: 'Build and grow your wealth with our expert guidance',
      benefits: [
        'Professional Portfolio Management',
        'Investment Advisory',
        'Retirement Planning',
        'Wealth Management'
      ]
    },
    {
      title: 'Insurance Solutions',
      icon: 'bi bi-shield-check',
      description: 'Protect what matters most with our insurance plans',
      benefits: [
        'Life Insurance',
        'Health Insurance',
        'Property Insurance',
        'Travel Insurance'
      ]
    },
    {
      title: 'International Banking',
      icon: 'bi bi-globe',
      description: 'Global banking solutions for international needs',
      benefits: [
        'Foreign Exchange',
        'International Transfers',
        'Multi-Currency Accounts',
        'Trade Services'
      ]
    }
  ];

  premiumServices = [
    {
      title: 'Private Banking',
      icon: 'bi bi-gem',
      perks: ['Dedicated Relationship Manager', 'Exclusive Investment Opportunities', 'Premium Credit Cards']
    },
    {
      title: 'Corporate Banking',
      icon: 'bi bi-building',
      perks: ['Corporate Financing', 'Cash Management', 'Advisory Services']
    }
  ];
}
