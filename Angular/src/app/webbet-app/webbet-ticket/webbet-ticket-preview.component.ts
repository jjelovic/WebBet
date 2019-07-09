import { WebbetTicket } from 'src/app/shared/webbet-ticket.model';
import { WebbetTicketMatch } from './../../shared/webbet-ticket-match.model';
import { WebbetMatches } from './../../shared/webbet-matches.model';
import { Component, OnInit } from '@angular/core';
import { WebbetTicketService } from 'src/app/shared/webbet-ticket.service';
import { WebbetAppService } from 'src/app/shared/webbet-app.service';


@Component({
  selector: 'app-webbet-ticket-preview',
  templateUrl: './webbet-ticket-preview.component.html',
  styleUrls: []
})
export class WebbetTicketPreviewComponent implements OnInit {

  constructor(private ticketService : WebbetTicketService, private service : WebbetAppService) { }

ticketMatchesForPreview: WebbetTicketMatch[]
ticketList: WebbetTicket[];

  ngOnInit() {

    this.service.getAllTickets().subscribe(res => {
      this.ticketList = res as WebbetTicket[];
   
      this.ticketService.ticketMatchesForPreview = this.ticketList[0].ticketMatches;
      this.ticketService.acitveTicketCode = this.ticketList[0].ticketCode;
  })

  }

}
