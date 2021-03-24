#include <stdio.h>
#include <sys/types.h>
#include <sys/stat.h>

#include <fcntl.h>

int main (int argc, char *argv[])
{
        int fd;
        if ((fd = open(argv[1], O_RDWR)) == -1)
                perror(argv[1]);
        printf("File %s opened\n", argv[1]);
        closed(fd);
        exit(0);
}
