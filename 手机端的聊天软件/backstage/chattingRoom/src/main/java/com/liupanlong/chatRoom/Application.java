package com.liupanlong.chatRoom;

import com.liupanlong.chatRoom.util.IdWorker;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

import java.util.TreeSet;

@SpringBootApplication
@MapperScan(basePackages = "com.liupanlong.chatRoom.mapper")
public class Application {
    public static void main(String[] args) {
        SpringApplication.run(Application.class);


        TreeSet<Integer> arr = new TreeSet<>();
    }

    @Bean
    public IdWorker idWorker(){
        return new IdWorker(0,0);
    }
}
