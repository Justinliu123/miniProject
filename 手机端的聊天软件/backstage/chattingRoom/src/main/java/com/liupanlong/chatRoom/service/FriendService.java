package com.liupanlong.chatRoom.service;

import com.liupanlong.chatRoom.pojo.vo.FriendReq;
import com.liupanlong.chatRoom.pojo.vo.User;
import org.springframework.stereotype.Service;

import java.util.List;

public interface FriendService {
    /**
     * 发送好友请求
     * @param fromUserid 申请好友的用户id
     * @param toUserid 要添加的好友id
     */
    void sendRequest(String fromUserid, String toUserid);

    /**
     * 根据用户id查询他对应的好友请求
     * @param userid 当前登录用户
     * @return 好友请求列表
     */
    List<FriendReq> findFriendReqByUserid(String userid);

    /**
     * 接收好友请求
     * @param reqid 好友请求id
     */
    void acceptFriendReq(String reqid);

    /**
     * 忽略好友请求
     * @param reqid 好友请求的id
     */
    void ignoreFriendReq(String reqid);

    /**
     * 查询我的好友
     * @param userid 当前登录的用户id
     * @return 好友列表
     */
    List<User> findFriendByUserid(String userid);
}
