import { Component } from '@angular/core';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent {
  teamMembers = [
    {
      name: 'Bhavy Choudhary',
      image: 'assets/images/team1.jpg'
    },
    {
      name: 'Yashasvi Pandya',
      image: 'assets/images/team2.jpg'
    },
    {
      name: 'Shraddha Butale',
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
