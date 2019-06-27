import { WebbetMatches } from './webbet-matches.model';
import { WebbetOffer } from './webbet-offer.model';
import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WebbetAppService {
  readonly rootURL = 'http://localhost:51157/api';

  constructor(private http: HttpClient) { }

  getWebbetMatches(): Observable<any> {
    return this.http.get(this.rootURL + '/Matches');
  }
}