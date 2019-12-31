
//app.component.ts
import { Component, ViewChild, OnInit, AfterViewInit } from '@angular/core';

import { MatDialog, MatTable, MatSort, MatTableDataSource, MatPaginator } from '@angular/material';
import { AuthenticationService } from 'src/app/shared/Services/AuthenticationService';
import Swal from 'sweetalert2';
declare var $: any;

export interface UsersData {
  name: string;
  id: number;
}

const ELEMENT_DATA: any[] = [
];
@Component({
  selector: 'app-team-management',
  templateUrl: './team-management.component.html',
  styleUrls: ['./team-management.component.css']
})
export class TeamManagementComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['name', 'action'];
  name: string = '';
  isSubmitted = false;
  isAlreadyExsit = false;
  public dataSource = new MatTableDataSource<any>();
  constructor(public dialog: MatDialog, private authService: AuthenticationService) { }
  ngOnInit() {
    this.authService.GetTeams().then((data: any) => {

      this.dataSource.data = data.data;

    })
  }
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatTable, { static: true }) table: MatTable<any>;



  modelChanged(newObj) {
    this.isSubmitted = false;
    this.isAlreadyExsit = false;
  }
  addRowData() {
    this.isSubmitted = true;
    if (this.name.trim() == '')
      return;
    this.authService.AddTeam(this.name).then((data) => {
      debugger;
      if (data.status == 1) {
        this.dataSource.data.push({
          name: this.name,
          id: data.data.id,
          isDeleted: false

        });
        this.dataSource.data = this.dataSource.data.sort();
        this.table.renderRows();
        debugger;
        $("#bntClose").click();
        Swal.fire('', 'Your team added successfully', 'success');

      } else if (data.status == 6) {
        this.isAlreadyExsit = true;
      }
    });


  }
  updateRowData(row_obj) {
    // this.dataSource = this.dataSource.filter((value, key) => {
    //   if (value.id == row_obj.id) {
    //     value.name = row_obj.name;
    //   }
    //   return true;
    // });
  }
  Delete(row_obj) {
    Swal.fire({
      title: '',
      text: "Are you sure to delete this team",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.value) {

        this.authService.DeleteTeam(row_obj.id).then(() => {
          this.dataSource.data = this.dataSource.data.filter((value, key) => {
            return value.id != row_obj.id;
          });
        });

      }
    });

  }
  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  }

}