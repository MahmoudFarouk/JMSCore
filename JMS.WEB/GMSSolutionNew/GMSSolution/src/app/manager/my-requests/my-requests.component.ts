import { Component, OnInit } from '@angular/core';
import { RequestService } from 'src/app/shared/Services/RequestsService';
import { RequestModel } from 'src/app/shared/Models/RequestModel';


@Component({
  selector: 'app-my-requests',
  templateUrl: './my-requests.component.html',
  styleUrls: ['./my-requests.component.css']
})

export class MyRequestsComponent implements OnInit {

  constructor(private requestService: RequestService) { }
  requests= [];

  ngOnInit() {
    this.requestService.getRequests().subscribe(result => {
      this.requests = result;
    });;
  }
}
