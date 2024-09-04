using haveaseat.DbContexts;
using haveaseat.DTOs;
using Microsoft.EntityFrameworkCore;

namespace haveaseat_api.Seeders;

public static class MapSeeder
{

    private static List<Cell> map = new List<Cell>();

    private static class Border
    {
        public const string Left = "-4px 0px 0px 0px black";
        public const string TopLeft = "-2px -2px 0px 2px black";
        public const string Top = "0px -4px 0px 0px black";
        public const string None = "none";
        public const string Right = "4px 0px 0px 0px black";
        public const string Bottom = "0px 4px 0px 0px black";
        public const string TopRight = "2px -2px 0px 2px black";
        public const string BottomLeft = "-2px 2px 0px 2px black";
        public const string BottomRight = "2px 2px 0px 2px black";
    }
    public static WebApplication SeedMap(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            using var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            try
            {
                //check if database exists
                context.Database.EnsureCreated();
                
                //seed only missing rooms

                List<Room> rooms = new List<Room>
                {
                    new Room { Id = 1, Name = "2.7" },
                    new Room { Id = 2, Name = "2.8" },
                    new Room { Id = 3, Name = "2.9" },
                    new Room { Id = 4, Name = "2.10" },
                    new Room { Id = 5, Name = "2.11" },
                    new Room { Id = 6, Name = "2.12" },
                    new Room { Id = 7, Name = "2.13" },
                    new Room { Id = 8, Name = "2.14" },
                    new Room { Id = 9, Name = "2.15" },
                    new Room { Id = 10, Name = "2.16" },
                    new Room { Id = 11, Name = "2.17" },
                    new Room { Id = 12, Name = "2.18" }
                };

                HashSet<string> existingRooms = context.Rooms.Select(r => r.Name).ToHashSet();
                List<Room> newRooms = rooms.Where(room => !existingRooms.Contains(room.Name)).ToList();
                
                if (newRooms.Any())
                {
                    context.Rooms.AddRange(newRooms);
                    context.SaveChanges();
                }
                
                // room 2.7
                if (context.Cells.FirstOrDefault(c => c.RoomId == 1) == null)
                {

                    for (int y = 1; y <= 4; y++)
                    {
                        for (int x = 1; x <= 2; x++)
                        {
                            string border = Border.None;
                            if (y == 1 && x == 1)
                            {
                                border = Border.TopLeft;
                            }
                            
                            else if (y == 4 && x == 1)
                            {
                                border = Border.BottomLeft;
                            }

                            else if (y == 1 && x == 2)
                            {
                                border = Border.TopRight;
                            }
                            
                            else if (x == 1)
                            {
                                border = Border.Left;
                            }

                            else if (y == 1)
                            {
                                border = Border.Top;
                            }

                            else if (x == 2)
                            {
                                border = Border.Right;
                            }
                            


                            map.Add(new Cell
                            {
                                PositionX = x,
                                PositionY = y,
                                Border = border,
                                RoomId = 1,
                            });
                        }
                    }
                }

                //room 2.8
                if (context.Cells.FirstOrDefault(c => c.RoomId == 2) == null)
                {

                    for (int y = 1; y <= 4; y++)
                    {
                        for (int x = 3; x <= 4; x++)
                        {
                            string border = Border.None;
                            if (y == 1 && x == 3)
                            {
                                border = Border.TopLeft;
                            }
                            else if (y == 4 && x == 4)
                            {
                                border = Border.BottomRight;
                            }

                            else if (y == 1 && x == 4)
                            {
                                border = Border.TopRight;
                            }
                            else if (x == 3)
                            {
                                border = Border.Left;
                            }

                            else if (y == 1)
                            {
                                border = Border.Top;
                            }

                            else if (x == 4)
                            {
                                border = Border.Right;
                            }

                            map.Add(new Cell
                            {
                                PositionX = x,
                                PositionY = y,
                                Border = border,
                                RoomId = 2,
                            });
                        }
                    }
                }
                
                // room 2.9
                if (context.Cells.FirstOrDefault(c => c.RoomId == 3) == null)
                {
                    for (int y = 1; y <= 6; y++)
                    {
                        for (int x = 5; x <= 6; x++)
                        {
                            string border = Border.None;
                            if (y == 1 && x == 5)
                            {
                                border = Border.TopLeft;
                            }
                            else if (y == 6 && x == 6)
                            {
                                border = Border.BottomRight;
                            }

                            else if (y == 1 && x == 6)
                            {
                                border = Border.TopRight;
                            }
                            
                            else if (x == 5)
                            {
                                border = Border.Left;
                            }

                            else if (y == 1)
                            {
                                border = Border.Top;
                            }

                            else if (x == 6)
                            {
                                border = Border.Right;
                            }
                            

                            map.Add(new Cell
                            {
                                PositionX = x,
                                PositionY = y,
                                Border = border,
                                RoomId = 3,
                            });
                        }
                    }
                }
                
                // room 2.10
                if (context.Cells.FirstOrDefault(c => c.RoomId == 4) == null)
                {

                    for (int y = 1; y <= 3; y++)
                    {
                        for (int x = 7; x <= 10; x++)
                        {
                            string border = Border.None;
                            if (y == 1 && x == 7)
                            {
                                border = Border.TopLeft;
                            }

                            else if (y == 1 && x == 10)
                            {
                                border = Border.TopRight;
                            }
                            
                            else if (y == 3 && x == 10)
                            {
                                border = Border.BottomRight;
                            }
                            
                            else if (x == 7)
                            {
                                border = Border.Left;
                            }

                            else if (y == 1)
                            {
                                border = Border.Top;
                            }
                            
                            else if (y == 3 && (x == 9 || x == 10))
                            {
                                border = Border.Bottom;
                            }

                            map.Add(new Cell
                            {
                                PositionX = x,
                                PositionY = y,
                                Border = border,
                                RoomId = 4,
                            });
                        }
                    }
                    
                    for (int y = 4; y <= 6; y++)
                    {
                        for (int x = 7; x <= 8; x++)
                        {
                            string border = Border.None;
                            
                            if (y == 6 && x == 7)
                            {
                                border = Border.BottomLeft;
                            }
                            
                            else if (x == 7)
                            {
                                border = Border.Left;
                            }
                            
                            else if (x == 8)
                            {
                                border = Border.Right;
                            }
                            

                            map.Add(new Cell
                            {
                                PositionX = x,
                                PositionY = y,
                                Border = border,
                                RoomId = 4,
                            });
                        }
                    }
                }
                
                // room 2.11
                if (context.Cells.FirstOrDefault(c => c.RoomId == 5) == null)
                {

                    for (int y = 1; y <= 3; y++)
                    {
                        for (int x = 11; x <= 13; x++)
                        {
                            string border = Border.None;
                            if (y == 1 && x == 11)
                            {
                                border = Border.TopLeft;
                            }

                            else if (y == 1 && x == 13)
                            {
                                border = Border.TopRight;
                            }
                            
                            else if (y == 3 && x == 11)
                            {
                                border = Border.BottomLeft;
                            }
                            
                            else if (x == 13)
                            {
                                border = Border.Right;
                            }

                            else if (y == 1)
                            {
                                border = Border.Top;
                            }

                            map.Add(new Cell
                            {
                                PositionX = x,
                                PositionY = y,
                                Border = border,
                                RoomId = 4,
                            });
                        }
                    }
                    
                    for (int y = 4; y <= 6; y++)
                    {
                        for (int x = 12; x <= 13; x++)
                        {
                            string border = Border.None;
                            
                            if (y == 6 && x == 13)
                            {
                                border = Border.BottomRight;
                            }
                            
                            else if (x == 12)
                            {
                                border = Border.Left;
                            }
                            
                            else if (x == 13)
                            {
                                border = Border.Right;
                            }
                            

                            map.Add(new Cell
                            {
                                PositionX = x,
                                PositionY = y,
                                Border = border,
                                RoomId = 5,
                            });
                        }
                    }
                }
                
                // room 2.12
                 if (context.Cells.FirstOrDefault(c => c.RoomId == 6) == null)
                 {
                 
                     for (int y = 1; y <= 4; y++)
                     {
                         for (int x = 14; x <= 17; x++)
                         {
                             string border = Border.None;
                             if (y == 1 && x == 14)
                             {
                                 border = Border.TopLeft;
                             }
                 
                             else if (y == 1 && x == 17)
                             {
                                 border = Border.TopRight;
                             }
                             
                             else if (y == 4 && x == 17)
                             {
                                 border = Border.BottomRight;
                             }
                             
                             else if (x == 17)
                             {
                                 border = Border.Right;
                             }
                             
                             else if (x == 14)
                             {
                                 border = Border.Left;
                             }
                 
                             else if (y == 1)
                             {
                                 border = Border.Top;
                             }
                             
                             else if (x == 16 && y == 4)
                             {
                                 border = Border.Bottom;
                             }
                 
                             map.Add(new Cell
                             {
                                 PositionX = x,
                                 PositionY = y,
                                 Border = border,
                                 RoomId = 6,
                             });
                         }
                     }
                     
                 
                     for (int y = 5; y <= 6; y++)
                     {
                         for (int x = 14; x <= 15; x++)
                         {
                             string border = Border.None;
                         
                             if (y == 6 && x == 14)
                             {
                                 border = Border.BottomLeft;
                             }
                         
                             else if (x == 15 && y == 6)
                             {
                                 border = Border.Right;
                             }
                         
                             else if (x == 14)
                             {
                                 border = Border.Left;
                             }
                         
                     
                             map.Add(new Cell
                             {
                                 PositionX = x,
                                 PositionY = y,
                                 Border = border,
                                 RoomId = 6,
                             });
                         }
                     }
                 
                 }

                // room 2.13
                if (context.Cells.FirstOrDefault(c => c.RoomId == 7) == null)
                {
                
                    for (int y = 5; y <= 12; y++)
                    {
                        for (int x = 16; x <= 17; x++)
                        {
                            string border = Border.None;
                            
                            if (y == 12 && x == 16)
                            {
                                border = Border.BottomLeft;
                            }
                            
                            else if (y == 5 && x == 17)
                            {
                                border = Border.TopRight;
                            }
                            
                            
                            else if (y == 12 && x == 17)
                            {
                                border = Border.BottomRight;
                            }
                            
                            
                            else if (x == 17)
                            {
                                border = Border.Right;
                            }
                
                            else if (y == 5)
                            {
                                border = Border.Top;
                            }
                            
                            else if (x == 16 && new []{6,8,9,11}.Contains(y))
                            {
                                border = Border.Left;
                            }
                
                            map.Add(new Cell
                            {
                                PositionX = x,
                                PositionY = y,
                                Border = border,
                                RoomId = 7,
                            });
                        }
                    }
                }
                
                // room 2.14
                if (context.Cells.FirstOrDefault(c => c.RoomId == 8) == null)
                {
                    for (int y = 9; y <= 12; y++)
                    {
                        for (int x = 13; x <= 15; x++)
                        {
                            string border = Border.None;

                            if (x == 15 && y == 12)
                            {
                                border = Border.BottomRight;
                            }
                            
                            else if (x == 13 && y == 12)
                            {
                                border = Border.BottomLeft;
                            }
                            
                            else if (x == 13 && y == 9)
                            {
                                border = Border.TopLeft;
                            }
                            
                            else if (y == 9 && x == 14)
                            {
                                border = Border.Top;
                            }
                            
                            else if (x == 15 && new []{9,11,12}.Contains(y))
                            {
                                border = Border.Right;
                            }
                            
                            
                            else if (y == 12 && x == 14)
                            {
                                border = Border.Bottom;
                            }
                            
                            else if (x == 13)
                            {
                                border = Border.Left;
                            }
                            
                            
                            map.Add(new Cell
                            {
                                PositionX = x,
                                PositionY = y,
                                Border = border,
                                RoomId = 8,
                            });
                        }
                    }
                }
                
                // room 2.15
                if (context.Cells.FirstOrDefault(c => c.RoomId == 9) == null)
                {
                    for (int y = 9; y <= 12; y++)
                    {
                        for (int x = 9; x <= 12; x++)
                        {
                            string border = Border.None;

                            if (y == 12 && x == 9)
                            {
                                border = Border.BottomLeft;
                            }
                            
                            else if (y == 12 && x == 12)
                            {
                                border = Border.BottomRight;
                            }
                            
                            else if (y == 9 && x == 12)
                            {
                                border = Border.TopRight;
                            }
                            
                            else if (y == 9 && (x == 10 || x == 11))
                            {
                                border = Border.Top;
                            }
                            
                            else if (y == 12)
                            {
                                border = Border.Bottom;
                            }
                            
                            else if (x == 12)
                            {
                                border = Border.Right;
                            }
                            
                            else if (x == 9)
                            {
                                border = Border.Left;
                            }
                            
                            map.Add(new Cell
                            {
                                PositionX = x,
                                PositionY = y,
                                Border = border,
                                RoomId = 9,
                            });
                        }
                    }
                }

                // room 2.16
                if (context.Cells.FirstOrDefault(c => c.RoomId == 10) == null)
                {
                    for (int y = 9; y <= 12; y++)
                    {
                        for (int x = 6; x <= 8; x++)
                        {
                            string border = Border.None;

                            if (y == 12 && x == 8)
                            {
                                border = Border.BottomRight;
                            }
                            
                            else if (y == 12 && x == 6)
                            {
                                border = Border.BottomLeft;
                            }
                            
                            else if (y == 9 && x == 6)
                            {
                                border = Border.TopLeft;
                            }
                            
                            else if (y == 9 && x == 7)
                            {
                                border = Border.Top;
                            }
                            
                            else if (x == 8)
                            {
                                border = Border.Right;
                            }
                            
                            else if (y == 12)
                            {
                                border = Border.Bottom;
                            }
                            
                            else if (x == 6)
                            {
                                border = Border.Left;
                            }
                            
                            map.Add(new Cell
                            {
                                PositionX = x,
                                PositionY = y,
                                Border = border,
                                RoomId = 10,
                            });
                        }
                    }
                }
                
                // room 2.17
                if (context.Cells.FirstOrDefault(c => c.RoomId == 11) == null)
                {
                    for (int y = 9; y <= 12; y++)
                    {
                        int x = 5;
                        string border = Border.Right;
                        switch (y)
                        {
                            case 9: border = Border.TopRight;
                                break;
                            case 12: border = Border.BottomRight; 
                                break;
                        }
                        
                        map.Add(new Cell
                        {
                            PositionX = x,
                            PositionY = y,
                            Border = border,
                            RoomId = 11,
                        });
                    }

                    for (int y = 10; y <= 12; y++)
                    {
                        string border = Border.Left;
                        int x = 4;
                        
                        switch (y)
                        {
                            case 10: border = Border.TopLeft;
                                break;
                            case 12: border = Border.BottomLeft; 
                                break;
                        }
                        
                        map.Add(new Cell
                        {
                            PositionX = x,
                            PositionY = y,
                            Border = border,
                            RoomId = 11,
                        });
                    }
                }
                
                // room 2.18
                if (context.Cells.FirstOrDefault(c => c.RoomId == 12) == null)
                {
                    for (int y = 10; y <= 12; y++)
                    {
                        for (int x = 1; x <= 3; x++)
                        {
                            string border = Border.None;

                            if (y == 12 && x == 1)
                            {
                                border = Border.BottomLeft;
                            }
                            else if (y == 12 && x == 3)
                            {
                                border = Border.BottomRight;
                            }
                            else if (y == 10 && x == 3)
                            {
                                border = Border.TopRight;
                            }

                            else if (y == 10 && x > 1)
                            {
                                border = Border.Top;
                            }
                            else if (y == 12)
                            {
                                border = Border.Bottom;
                            }
                            else if (x == 3)
                            {
                                border = Border.Right;
                            }
                            else if (x == 1)
                            {
                                border = Border.Left;
                            }
                            
                            map.Add(new Cell
                            {
                                PositionX = x,
                                PositionY = y,
                                Border = border,
                                RoomId = 12,
                            });
                            
                        }
                    }
                }
                
                context.Cells.AddRange(map);
                context.SaveChanges();
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        return app;
    }
}