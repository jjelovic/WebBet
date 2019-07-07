import { WebbetTicket } from './../../shared/webbet-ticket.model';
import { Component, OnInit } from '@angular/core';
import { WebbetAppService } from 'src/app/shared/webbet-app.service';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-webbet-ticket-list',
  templateUrl: './webbet-ticket-list.component.html',
  styleUrls: []
})
export class WebbetTicketListComponent implements OnInit {

  ticketList: WebbetTicket[];

  constructor(
    public dialogRef: MatDialogRef<WebbetTicketListComponent>, 
    private service: WebbetAppService
  ) { }

  ngOnInit() {
    this.service.getAllTickets().subscribe(res => {
        this.ticketList = res as WebbetTicket[];
    })
  }

}
