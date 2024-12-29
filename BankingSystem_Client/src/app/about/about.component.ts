import { Component } from '@angular/core';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent {
  teamMembers = [
    {
      name: 'Sarah Johnson',
      position: 'Chief Executive Officer',
      description: 'With 20+ years of banking experience, Sarah leads our vision for modern banking.',
      image: 'assets/images/team1.jpg'
    },
    {
      name: 'Michael Chen',
      position: 'Chief Technology Officer',
      description: 'Leading our digital transformation with innovative banking solutions.',
      image: 'assets/images/team2.jpg'
    },
    {
      name: 'Emma Williams',
      position: 'Head of Customer Relations',
      description: 'Dedicated to ensuring exceptional banking experience for all customers.',
      image: 'assets/images/team3.jpg'
    }
  ];

  achievements = [
    {
      number: '25+',
      title: 'Years of Excellence',
      description: 'Serving customers with integrity and innovation'
    },
    {
      number: '500+',
      title: 'Branches Worldwide',
      description: 'Global presence with local expertise'
    },
    {
      number: '10M+',
      title: 'Happy Customers',
      description: 'Trusted by millions for their banking needs'
    },
    {
      number: '50+',
      title: 'Banking Awards',
      description: 'Recognized for outstanding banking services'
    }
  ];

}
