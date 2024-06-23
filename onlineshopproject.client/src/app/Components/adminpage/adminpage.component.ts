import { Component, OnInit } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { User } from '../../Models/User';
import { RoleService } from '../../Services/Role/role.service';
import { UserService } from '../../Services/User/user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-adminpage',
  templateUrl: './adminpage.component.html',
  styleUrls: ['./adminpage.component.css'],
  providers: [RoleService, UserService]
})
export class AdminpageComponent implements OnInit {
  public Users: any[] = [];
  public adminRoles: { [key: number]: number } = {}; 

  constructor(private _UserService: UserService, private _RoleService: RoleService) { }

  ngOnInit() {

    this._UserService.getUsers().subscribe(
      async response => {

        const jwt = localStorage.getItem("token");

        if (jwt != null) {

          const decodedJWT: any = jwtDecode(jwt);
          const UserName = decodedJWT.sub;

         
          const users = response.filter((user: User) => user.username !== UserName);

         
          for (const user of users) {
            try {
              const roleResponse = await this._RoleService.getUserRole(user.id).toPromise();
              if (roleResponse && roleResponse.roleId !== undefined) {
                this.adminRoles[user.id] = roleResponse.roleId; 
              }

            } catch (error) {
             
              console.log(error)
            }
          }

          this.Users = users;
        }
      },
      error => {
        console.log(error);
      }
    );
  }

  isAdmin(userId: number): boolean {

    if (this.adminRoles[userId] == 2) {
      return true
    } else {
      return false
    }

 
  }

  RemoveRole(userId: number) {

    this._RoleService.DeleteRole(userId).subscribe(
      response => {
  
          Swal.fire({
            icon: "success",
            text: "role removed successfully"
          }).then(() => {
            window.location.reload();
          });

      },

      error => {
        console.log(error)
        Swal.fire({
          icon: "error",
          text: "error when deleting the role"
        });

      }
    )
  

  }

  giveAdmin(userId: number) {
    this._RoleService.CreateRole(userId, 2).subscribe(
      response => {
        Swal.fire({
          icon: "success",
          text: "user is now an admin"
        }).then(() => {
          window.location.reload();
        });
      },
      error => {
        console.log(error);
        Swal.fire({
          icon: "error",
          text: "error when making the user an admin"
        });
      }
    );
  }

  deleteUser(userId: number) {
    Swal.fire({
      title: '¿are you sure?',
      text: "¡you can't revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'delete it!',
      cancelButtonText: 'cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this._UserService.DeleteUser(userId).subscribe(
          response => {
            Swal.fire({
              icon: "success",
              text: "user deleted successfully"
            }).then(() => {
              window.location.reload();
            });
          },
          error => {
            Swal.fire({
              icon: "error",
              text: "error deleting the user"
            });
          }
        );
      }
    });
  }
}
