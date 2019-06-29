import { WebbetMatches } from './webbet-matches.model';
import { WebbetOffer } from './webbet-offer.model';
import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { WebbetWallet } from './webbet-wallet.model';

@Injectable({
  providedIn: 'root'
})
export class WebbetAppService {

  formData : WebbetWallet;
  readonly rootURL = 'http://localhost:51157/api';


  constructor(private http: HttpClient) { }

  getWebbetMatches(): Observable<any> {
    return this.http.get(this.rootURL + '/Matches');
  }

  postWalletDeposit(formData:WebbetWallet){
    return this.http.post(this.rootURL + '/Wallet', formData);
  }

  getWalletBalance():Observable<any> {
    return this.http.get(this.rootURL + '/Wallet');
  }
}