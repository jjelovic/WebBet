import { Component, OnInit } from '@angular/core';
import { WebbetAppService } from 'src/app/shared/webbet-app.service';
import { WebbetMatches } from 'src/app/shared/webbet-matches.model';
import { element } from '@angular/core/src/render3/instructions';

@Component({
  selector: 'app-webbet-offer',
  templateUrl: './webbet-offer.component.html',
  styles: []
})
export class WebbetOfferComponent implements OnInit {
  offerList: WebbetMatches[];
  topOfferList: WebbetMatches[];

  constructor(private service: WebbetAppService) { }

  ngOnInit() {
    this.service.getWebbetMatches().subscribe(res => {
        this.offerList = res as WebbetMatches[];

        this.topOfferList = this.offerList.filter((element) =>
                          element.matches.some((subElement) => subElement.isPartOfTopOffer))
                          .map(element => {
                          return Object.assign( {topOfferMatches: element.matches.filter(subElement => subElement.isPartOfTopOffer )})
        });

        console.log(this.topOfferList);
      });
  }

}
