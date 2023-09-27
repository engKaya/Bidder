import { DialogRef } from '@angular/cdk/dialog';
import { Injectable } from '@angular/core';  
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';


@Injectable({
  providedIn: 'root',
})


export class DialogService {  
    size: DialogSize = DialogSize.Medium;
    config : MatDialogConfig = {
        
    }
    constructor(
        public dialog: MatDialog
    ) {}
    sizes = {
        "sm": {
            width: '20%',
            height: '15%'
        },
        "md": {
            width: '35%',
            height: '30%'
        },
        "lg": {
            width: '45%',
            height: '40%'
        },
        "xlg": {
            width: '55%',
            height: '50%'
        },
        "xxl": {
            width: '75%',
            height: '70%'
        },
        "full": {
            width: '100%',
            height: '100%'
        }
    }
    
    openDialog(component: any, size: DialogSize = DialogSize.Medium ,data: any = null, afterClosed: Function | undefined = undefined) : MatDialogRef<any>  {
        var sizeConfig = this.sizes[size];
        const dialogRef = this.dialog.open(component, {
            width: sizeConfig.width,
            height: sizeConfig.height,
            data: data === null ? {} : data
        },);

        if (afterClosed) {
            dialogRef.afterClosed().subscribe(result => {
                afterClosed(result);
            });
        }  

        return dialogRef;
    } 
}

export enum DialogSize {
    Small = 'sm',
    Medium = 'md',
    Large = 'lg',
    ExtraLarge = 'xlg',
    FullLarge = 'xxl',
    Full = 'full'
}

