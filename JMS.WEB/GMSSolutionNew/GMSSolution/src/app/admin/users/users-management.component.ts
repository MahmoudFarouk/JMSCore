import { Component, ViewChild, OnInit, AfterViewInit } from '@angular/core';
import { MatDialog, MatTable, MatSort, MatTableDataSource, MatPaginator } from '@angular/material';
import { AuthenticationService } from 'src/app/shared/Services/AuthenticationService';
import Swal from 'sweetalert2';
declare var $: any;
@Component({
    selector: 'app-users-management',
    templateUrl: './users-management.component.html',
    styleUrls: ['./users-management.component.css']
})
export class UsersManagementComponent implements OnInit {


    displayedColumns: string[] = ['username', 'fullName'];
    Email: string = '';
    Fullname: string = '';
    TeamId?: string = null;
    WorkForceId?: string = null;
    LicenseExpiryDate: Date = null;
    LicenseNo: string = '';
    TrainingDetails: string = '';
    GatePassStatus: string = '';
    IsActive: boolean = true;
    Password: string = '';
    ConfrimPassword: string = '';
    Teams = [];
    WorkForces = [];
    isSubmitted = false;
    isAlreadyExsit = false;
    public dataSource = new MatTableDataSource<any>();
    constructor(public dialog: MatDialog, private authService: AuthenticationService) { }
    ngOnInit() {
        this.authService.GetAllUsers().then((data: any) => {

            this.dataSource.data = data.data;

        });
        this.authService.GetTeams().then((data: any) => {
            this.Teams = data.data;
        });
        this.authService.GetWorkFoces().then((data: any) => {
            this.WorkForces = data.data;
        });
    }
    @ViewChild(MatSort, { static: true }) sort: MatSort;
    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatTable, { static: true }) table: MatTable<any>;



    // modelChanged(newObj) {
    //   this.isSubmitted = false;
    //   this.isAlreadyExsit = false;
    // }
    validateEmail() {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(this.Email);
    }
    addRowData() {
        this.isSubmitted = true;
        if (this.Email.trim() == '')
            return;
        if (!this.validateEmail()) {
            return;
        }
        if (this.Email.trim() == '')
            return;
        if (this.TeamId == null)
            return;
        if (this.WorkForceId == null)
            return;
        if (this.LicenseNo == '')
            return;
        if (this.LicenseExpiryDate == null)
            return;
        if (this.Password == '')
            return;
        if (this.ConfrimPassword == '')
            return;
        if (this.Password != this.ConfrimPassword)
            return;
        if (this.Password.length < 8)
            return;
        this.authService.Register({

            Username: this.Email,
            Password: this.Password,
            FullName: this.Fullname,
            UserGroupId: this.TeamId,
            UserWorkForceId: this.WorkForceId,
            LicenseNo: this.LicenseNo,
            LicenseExpiryDate: this.LicenseExpiryDate,
            TrainingDetails: this.TrainingDetails,
            GatePassStatus: this.GatePassStatus,
            RoleId: 'f598692d-ecba-47ef-8b07-e36d778b9baf'

        }).then((data) => {
            $("#bntClose").click();
            this.authService.GetAllUsers().then((data: any) => {

                this.dataSource.data = data.data;
                this.table.renderRows();
            });
            Swal.fire('', 'Your Driver added successfully', 'success');
        });
        /* this.authService.AddWorkForce(this.name).then((data) => {
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
             Swal.fire('', 'Your Workforce added successfully', 'success');
     
           } else if (data.status == 6) {
             this.isAlreadyExsit = true;
           }
         });*/


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
            text: "Are you sure to delete this workforce",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.value) {

                this.authService.DeleteWorkForce(row_obj.id).then(() => {
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