export interface Room {
    id: number;
    name: string;
    cells: Cell[];
    desks: Desk[];
  }
  export interface Cell {
    imagePath: any;
    id: number;
    positionX: number;
    positionY: number;
    border: string;
    isDesk?: boolean;
    rotationClass?: string;
    isReserved?: boolean;
    isClicked?: boolean;
    isUsers?: boolean;
    isNew?: boolean;
    isDeleted?: boolean;
  }
  export interface AddDesk {
    positionX: number;
    positionY: number;
    chairPosition: number;
    rotation?: number;
  }
  export interface Desk {
    id: number;
    positionX: number;
    positionY: number;
    chairPosition: number;
    rotation?: number;
  }
  export interface Reservation {
    id: number;
    date: string;
    desk: Desk;
    user: User;
  }
  export interface NewReservation {
    date: string;
    userId: number;
    deskId: number;
  }
  
  export interface User {
    id: number;
    email: string;
    name: string;
    surname: string;
    role: number;
    reservationId: number;
  }

  export interface ForbiddenDate {
    id: number;
    description: string;
    date: Date;
  }