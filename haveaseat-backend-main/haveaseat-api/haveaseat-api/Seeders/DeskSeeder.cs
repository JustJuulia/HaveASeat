using haveaseat.DbContexts;
using haveaseat.Models;
using Microsoft.EntityFrameworkCore;

namespace haveaseat_api.Seeders;

public static class DeskSeeder
{

    private static List<Desk> desks;
    
    public static WebApplication SeedDesks(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            using var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            try
            {
                context.Database.EnsureCreated();
                if (!context.Desks.Any())
                {
                    // room 2.7
                    context.Add(new Desk
                    {
                        PositionX = 1,
                        PositionY = 1,
                        ChairPosition = ChairPosition.TOP,
                        RoomId = 1
                    });
                    // room 2.8
                    context.Add(new Desk
                    {
                        PositionX = 4,
                        PositionY = 1,
                        ChairPosition = ChairPosition.TOP,
                        RoomId = 2
                    });
                    // room 2.9
                    context.AddRange(
                        new Desk
                        {
                            PositionX = 5,
                            PositionY = 1,
                            ChairPosition = ChairPosition.LEFT,
                            RoomId = 3
                        },
                        new Desk
                        {
                            PositionX = 6,
                            PositionY = 1,
                            ChairPosition = ChairPosition.RIGHT,
                            RoomId = 3
                        },
                        new Desk
                        {
                            PositionX = 5,
                            PositionY = 2,
                            ChairPosition = ChairPosition.LEFT,
                            RoomId = 3
                        },
                        new Desk
                        {
                            PositionX = 6,
                            PositionY = 2,
                            ChairPosition = ChairPosition.RIGHT,
                            RoomId = 3
                        },
                        new Desk
                        {
                            PositionX = 6,
                            PositionY = 4,
                            ChairPosition = ChairPosition.TOP,
                            RoomId = 3
                        }
                    );

                    // room 2.10
                    context.AddRange(
                        new Desk
                        {
                            PositionX = 7,
                            PositionY = 1,
                            ChairPosition = ChairPosition.TOP,
                            RoomId = 4
                        },
                        new Desk
                        {
                            PositionX = 7,
                            PositionY = 2,
                            ChairPosition = ChairPosition.BOTTOM,
                            RoomId = 4
                        },
                        new Desk
                        {
                            PositionX = 7,
                            PositionY = 4,
                            ChairPosition = ChairPosition.TOP,
                            RoomId = 4
                        },
                        new Desk
                        {
                            PositionX = 9,
                            PositionY = 1,
                            ChairPosition = ChairPosition.LEFT,
                            RoomId = 4
                        },
                        new Desk
                        {
                            PositionX = 9,
                            PositionY = 2,
                            ChairPosition = ChairPosition.LEFT,
                            RoomId = 4
                        },
                        new Desk
                        {
                            PositionX = 10,
                            PositionY = 1,
                            ChairPosition = ChairPosition.RIGHT,
                            RoomId = 4
                        },
                        new Desk
                        {
                            PositionX = 10,
                            PositionY = 2,
                            ChairPosition = ChairPosition.RIGHT,
                            RoomId = 4
                        }
                    );

                    // room 2.11
                    context.AddRange(
                        new Desk
                        {
                            PositionX = 11,
                            PositionY = 1,
                            ChairPosition = ChairPosition.LEFT,
                            RoomId = 5
                        },
                        new Desk
                        {
                            PositionX = 11,
                            PositionY = 2,
                            ChairPosition = ChairPosition.LEFT,
                            RoomId = 5
                        },
                        new Desk
                        {
                            PositionX = 12,
                            PositionY = 1,
                            ChairPosition = ChairPosition.RIGHT,
                            RoomId = 5
                        },
                        new Desk
                        {
                            PositionX = 12,
                            PositionY = 2,
                            ChairPosition = ChairPosition.RIGHT,
                            RoomId = 5
                        },
                        new Desk
                        {
                            PositionX = 13,
                            PositionY = 4,
                            ChairPosition = ChairPosition.TOP,
                            RoomId = 5
                        },
                        new Desk
                        {
                            PositionX = 13,
                            PositionY = 5,
                            ChairPosition = ChairPosition.BOTTOM,
                            RoomId = 5
                        }
                    );

                    // room 2.12
                    context.AddRange(
                        new Desk
                        {
                            PositionX = 14,
                            PositionY = 1,
                            ChairPosition = ChairPosition.TOP,
                            RoomId = 6
                        },
                        new Desk
                        {
                            PositionX = 14,
                            PositionY = 2,
                            ChairPosition = ChairPosition.BOTTOM,
                            RoomId = 6
                        },
                        new Desk
                        {
                            PositionX = 16,
                            PositionY = 1,
                            ChairPosition = ChairPosition.TOP,
                            RoomId = 6
                        },
                        new Desk
                        {
                            PositionX = 17,
                            PositionY = 1,
                            ChairPosition = ChairPosition.TOP,
                            RoomId = 6
                        },
                        new Desk
                        {
                            PositionX = 16,
                            PositionY = 2,
                            ChairPosition = ChairPosition.BOTTOM,
                            RoomId = 6
                        },
                        new Desk
                        {
                            PositionX = 17,
                            PositionY = 2,
                            ChairPosition = ChairPosition.BOTTOM,
                            RoomId = 6
                        },
                        new Desk
                        {
                            PositionX = 16,
                            PositionY = 4,
                            ChairPosition = ChairPosition.TOP,
                            RoomId = 6
                        },
                        new Desk
                        {
                            PositionX = 17,
                            PositionY = 4,
                            ChairPosition = ChairPosition.TOP,
                            RoomId = 6
                        }
                    );

                    // room 2.13
                    context.AddRange(
                        new Desk
                        {
                            PositionX = 16,
                            PositionY = 5,
                            ChairPosition = ChairPosition.LEFT,
                            RoomId = 7
                        },
                        new Desk
                        {
                            PositionX = 17,
                            PositionY = 5,
                            ChairPosition = ChairPosition.RIGHT,
                            RoomId = 7
                        },
                        new Desk
                        {
                            PositionX = 17,
                            PositionY = 7,
                            ChairPosition = ChairPosition.TOP,
                            RoomId = 7
                        },
                        new Desk
                        {
                            PositionX = 17,
                            PositionY = 8,
                            ChairPosition = ChairPosition.BOTTOM,
                            RoomId = 7
                        },
                        new Desk
                        {
                            PositionX = 17,
                            PositionY = 11,
                            ChairPosition = ChairPosition.TOP,
                            RoomId = 7
                        },
                        new Desk
                        {
                            PositionX = 17,
                            PositionY = 12,
                            ChairPosition = ChairPosition.BOTTOM,
                            RoomId = 7
                        }
                    );

                    // room 2.14
                    context.AddRange(
                        new Desk
                        {
                            PositionX = 13,
                            PositionY = 11,
                            ChairPosition = ChairPosition.LEFT,
                            RoomId = 8
                        },
                        new Desk
                        {
                            PositionX = 13,
                            PositionY = 12,
                            ChairPosition = ChairPosition.LEFT,
                            RoomId = 8
                        },

                        new Desk
                        {
                            PositionX = 14,
                            PositionY = 11,
                            ChairPosition = ChairPosition.RIGHT,
                            RoomId = 8
                        },

                        new Desk
                        {
                            PositionX = 14,
                            PositionY = 12,
                            ChairPosition = ChairPosition.RIGHT,
                            RoomId = 8
                        }
                    );

                    // room 2.15
                    context.AddRange(
                        new Desk
                        {
                            PositionX = 9,
                            PositionY = 11,
                            ChairPosition = ChairPosition.LEFT,
                            RoomId = 9
                        },
                        new Desk
                        {
                            PositionX = 9,
                            PositionY = 12,
                            ChairPosition = ChairPosition.LEFT,
                            RoomId = 9
                        },
                        new Desk
                        {
                            PositionX = 10,
                            PositionY = 11,
                            ChairPosition = ChairPosition.RIGHT,
                            RoomId = 9
                        },
                        new Desk
                        {
                            PositionX = 10,
                            PositionY = 12,
                            ChairPosition = ChairPosition.RIGHT,
                            RoomId = 9
                        },
                        new Desk
                        {
                            PositionX = 12,
                            PositionY = 9,
                            ChairPosition = ChairPosition.TOP,
                            RoomId = 9
                        }
                    );

                    // room 2.16
                    context.AddRange(
                        new Desk
                        {
                            PositionX = 6,
                            PositionY = 11,
                            ChairPosition = ChairPosition.LEFT,
                            RoomId = 10
                        },
                        new Desk
                        {
                            PositionX = 6,
                            PositionY = 12,
                            ChairPosition = ChairPosition.LEFT,
                            RoomId = 10
                        },
                        new Desk
                        {
                            PositionX = 7,
                            PositionY = 11,
                            ChairPosition = ChairPosition.RIGHT,
                            RoomId = 10
                        },
                        new Desk
                        {
                            PositionX = 7,
                            PositionY = 12,
                            ChairPosition = ChairPosition.RIGHT,
                            RoomId = 10
                        }
                    );

                    // room 2.17
                    context.Add(
                        new Desk
                        {
                            PositionX = 5,
                            PositionY = 12,
                            ChairPosition = ChairPosition.RIGHT,
                            RoomId = 11
                        });

                    // room 2.18
                    context.Add(
                        new Desk
                        {
                            PositionX = 3,
                            PositionY = 12,
                            ChairPosition = ChairPosition.RIGHT,
                            RoomId = 12
                        });

                    context.SaveChanges();
                }
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