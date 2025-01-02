import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  center: google.maps.LatLngLiteral = { lat: 23.1825, lng: 75.7781 };
  zoom = 15;
  markers: { position: google.maps.LatLngLiteral, label: string }[] = [
    {
      position: { lat: 23.1825, lng: 75.7781 },
      label: 'Infinity Bank'
    }
  ];

  apiLoaded: Observable<boolean>;

  constructor(private httpClient: HttpClient) {
    const apiKey = 'YOUR_GOOGLE_MAPS_API_KEY';
    this.apiLoaded = this.httpClient.jsonp(
      `https://maps.googleapis.com/maps/api/js?key=${apiKey}`,
      'callback'
    ).pipe(
      map(() => true),
      catchError(() => of(false))
    );
  }

  ngOnInit(): void {
    // Additional initialization logic if needed
  }
}
